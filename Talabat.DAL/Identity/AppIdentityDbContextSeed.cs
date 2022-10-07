using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using Talabat.DAL.Entities.Identity;

namespace Talabat.DAL.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "Mostafa Salah",
                    UserName = "mostafasalah100",
                    Email = "mostafasalah100@gmail.com",
                    PhoneNumber = "01020000000",
                    Address = new Address()
                    {
                        FirstName = "Mostafa",
                        LastName = "Salah",
                        Country = "Egypt",
                        City = "Cairo",
                        Street = "10 Tahrir st",
                    },
                };

                await userManager.CreateAsync(user , "P@ssw0rd");
            }
        }
    }
}
