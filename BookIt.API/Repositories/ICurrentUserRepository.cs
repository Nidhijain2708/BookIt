using BookIt.API.Models.Domain;
using Microsoft.AspNetCore.Identity;

namespace BookIt.API.Repositories
{
    public interface ICurrentUserRepository
    {
        Task<IdentityUser> GetByIdAsync(string id);
    }
}
