using Microsoft.AspNetCore.Identity;

namespace BookIt.API.Repositories
{
    public interface ITokenRepository
    {
        string createJWTToken(IdentityUser user);
    }
}
