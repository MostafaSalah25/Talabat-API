using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.BLL.Interfaces;
using Talabat.DAL.Entities.Identity;

namespace Talabat.BLL.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;

        public TokenService(IConfiguration configuration) // to access section JWT in appset
        {
            this.configuration = configuration;
        }
        public async Task<string> CreateToken(AppUser user , UserManager<AppUser> userManager)
        {
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.DisplayName),
            };

            var userRoles = await userManager.GetRolesAsync(user); 
            foreach (var role in userRoles)
                authClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));

            // create Key..
            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])); 

            var token = new JwtSecurityToken(   
                // Registered Claims
                issuer: configuration["JWT:ValidIssuer"] ,
                audience: configuration["JWT:ValidAudience"] ,
                expires: DateTime.Now.AddDays(double.Parse(configuration["JWT:DurationInDays"])), 
                // Private Claims
                claims: authClaims,
                // Key  > 
                signingCredentials: new SigningCredentials(authKey , SecurityAlgorithms.HmacSha256Signature)
                );

            return  new JwtSecurityTokenHandler().WriteToken(token); 
            // as send token specifications to WriteToken meth to create it


        }

    }
}
