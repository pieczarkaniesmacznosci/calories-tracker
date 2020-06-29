using API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.DbContexts
{
    public class CaloriesLibraryContext : DbContext
    {
        public CaloriesLibraryContext(DbContextOptions<CaloriesLibraryContext> options) : base(options)
        {
        }
        public DbSet<ProductEntity> Products{get; set;}
    }
}