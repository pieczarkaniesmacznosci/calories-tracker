using Entities;
using Microsoft.EntityFrameworkCore;

namespace UserAPI.DbContexts
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            SeedUserNutritionTable(modelBuilder);
            SeedUserWeightTable(modelBuilder);
        }

        private static void SeedUserWeightTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserWeight>().HasData(
                new UserWeight
                {
                    Id = 1,
                    UserId = 1,
                    Weight = 71.5,
                    Date = new DateTime(2020, 6, 1)
                }
            );
        }

        private static void SeedUserNutritionTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserNutrition>().HasData(
                new UserNutrition()
                {
                    Id = 1,
                    UserId = 1,
                    Kcal = 2070,
                    Protein = 142,
                    Carbohydrates = 246,
                    Fat = 57.51d,
                    Date = new DateTime(2020, 1, 1)
                }
            );
        }
    }
}
