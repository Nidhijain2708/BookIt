using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookIt.API.Data
{
    public class BookItAuthDbContext: IdentityDbContext
    {
        public BookItAuthDbContext(DbContextOptions<BookItAuthDbContext> options):base(options)
        {
            
        }
    }
}
