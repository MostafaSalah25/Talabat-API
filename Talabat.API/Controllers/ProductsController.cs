using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Talabat.API.Dtos;
using Talabat.API.Errors;
using Talabat.API.Helpers;
using Talabat.BLL.Interfaces;
using Talabat.BLL.Specifications;
using Talabat.BLL.Specifications.ProductSpecifications;
using Talabat.DAL.Entities;

namespace Talabat.API.Controllers
{
    // no one access Endpoints in this Cont except who is Authorized
    [Authorize] 
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productsRep; // using Design Pattern Repository 
        private readonly IUnitOfWork _unitOfWork; //using Unit of Work Des P
        private readonly IMapper _mapper;
        public ProductsController(IGenericRepository<Product> productsRep , IUnitOfWork unitOfWork , IMapper mapper)
        {
            _productsRep = productsRep;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //[CashedResponse(600)] // Cashing Attribute 
        [HttpGet] 
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams productParams)
        {
            //var products = await _productsRep.GetAllAsync(); // ret Prods without nav props ProdType ,Brand 'null' 
            // ret Nav Props work Eager L using Des Pat Specification 
            var spec = new ProductsWithTypesAndBrandsSpecification( productParams);
            //var products = await _productsRep.GetAllWithSpecAsync(spec); // using Design Pattern Repository 
            var products = await _unitOfWork.Repository<Product>().GetAllWithSpecAsync(spec);//using Unit of Work Des P

            var Data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
            // Pagination & Practical Count
            var countSpec = new ProductWithFiltersForCountSpecification(productParams); 
            var Count = await _unitOfWork.Repository<Product>().GetCountAsync(countSpec);

            var DataPagAndCounted = new Pagination<ProductToReturnDto>(productParams.PageIndex,productParams.PageSize,Count, Data);
            return Ok(DataPagAndCounted);  
        }  

        [HttpGet("{id}")] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse) , StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _unitOfWork.Repository<Product>().GetEntityWithSpecAsync(spec);

            var productDto = _mapper.Map<Product, ProductToReturnDto>(product);
            if(productDto==null)
                return  NotFound(new ApiResponse(404));
            return Ok(productDto);
        }

        [HttpGet("brands")] 
        public async Task<ActionResult<ProductBrand>> GetBrands()
        {
            var brands = await _unitOfWork.Repository<ProductBrand>().GetAllAsync(); 
            return Ok(brands);
        }
        [HttpGet("types")] 
        public async Task<ActionResult<ProductType>> GetTypes()
        {
            var types = await _unitOfWork.Repository<ProductType>().GetAllAsync();
            return Ok(types);
        }


    }
}
