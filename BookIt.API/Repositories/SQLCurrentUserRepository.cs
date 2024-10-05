using BookIt.API.Models.Domain;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace BookIt.API.Repositories
{
    public class SQLCurrentUserRepository: ICurrentUserRepository
    {
        private readonly UserManager<IdentityUser> userManager;

        public SQLCurrentUserRepository(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IdentityUser> GetByIdAsync(string id)
        {
            //id=id.ToString();
            var user = await userManager.FindByIdAsync(id);
            return user;
        }
    }
}
