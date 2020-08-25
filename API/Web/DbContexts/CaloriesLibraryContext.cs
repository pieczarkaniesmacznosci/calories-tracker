
using API.Web.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Web.DbContexts
{
    public class CaloriesLibraryContext : DbContext
    {
        public CaloriesLibraryContext(DbContextOptions<CaloriesLibraryContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<ProductEntity> Products{get; set;}
    }
}