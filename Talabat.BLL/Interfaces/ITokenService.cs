using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Talabat.DAL.Entities.Identity;

namespace Talabat.BLL.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser appUser , UserManager<AppUser> userManager);
    }
}
