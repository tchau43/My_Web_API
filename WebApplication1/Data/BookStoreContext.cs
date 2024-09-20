using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    public class BookStoreContext : IdentityDbContext<ApplicationUsers>
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options) { }

        #region DbSet
        public DbSet<Book>? Books { get; set; }
        #endregion
    }
}
