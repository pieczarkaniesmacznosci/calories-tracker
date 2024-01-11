using Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace DbContexts
{
    public class CaloriesDbContext : DbContext
    {
        private static readonly Guid _adminUserGuid = new("10000000-0000-0000-0000-000000000000");
        private static readonly Guid _userUserGuid = new("20000000-0000-0000-0000-000000000000");

        public CaloriesDbContext(DbContextOptions<CaloriesDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<MealLog> MealLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MealProduct>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<MealProduct>().HasOne(x => x.Product).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<MealLog>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<MealLog>().HasOne(x => x.Meal);
            modelBuilder.Entity<Meal>().HasMany(x => x.MealLogs);
            modelBuilder.Entity<Meal>().Property(x => x.Id).ValueGeneratedOnAdd();

            SeedProductTable(modelBuilder);
            SeedMealTable(modelBuilder);
            SeedMealLogTable(modelBuilder);
            SeedMealProductTable(modelBuilder);
        }

        private static void SeedProductTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    Id = 1,
                    UserId = _adminUserGuid,
                    Name = "Chicken",
                    Kcal = 111.0d,
                    Protein = 21.0d,
                    Carbohydrates = 0.0d,
                    Fat = 3.0d,
                    IsDefault = true,
                    IsAvailable = true,
                    DateAdded = DateTime.Now
                },
                new Product()
                {
                    Id = 2,
                    UserId = _adminUserGuid,
                    Name = "Rice",
                    Kcal = 339.5d,
                    Protein = 6.7d,
                    Carbohydrates = 76.6d,
                    Fat = 0.7d,
                    IsDefault = true,
                    IsAvailable = true,
                    DateAdded = DateTime.Now
                },
                new Product()
                {
                    Id = 3,
                    UserId = _adminUserGuid,
                    Name = "Milky Chocolate",
                    Kcal = 580.8d,
                    Protein = 7.7d,
                    Carbohydrates = 51.1d,
                    Fat = 38.4d,
                    IsDefault = true,
                    IsAvailable = true,
                    DateAdded = DateTime.Now
                },
                new Product()
                {
                    Id = 4,
                    UserId = _adminUserGuid,
                    Name = "White bread",
                    Kcal = 237.7d,
                    Protein = 4.0d,
                    Carbohydrates = 52.5d,
                    Fat = 1.3d,
                    IsDefault = true,
                    IsAvailable = true,
                    DateAdded = DateTime.Now
                },
                new Product()
                {
                    Id = 5,
                    UserId = _adminUserGuid,
                    Name = "Tomato ketchup",
                    Kcal = 94.5d,
                    Protein = 1.6d,
                    Carbohydrates = 20.0d,
                    Fat = 0.9d,
                    IsDefault = true,
                    IsAvailable = true,
                    DateAdded = DateTime.Now
                },
                new Product()
                {
                    Id = 6,
                    UserId = _adminUserGuid,
                    Name = "Gouda cheese",
                    Kcal = 288.7d,
                    Protein = 25.5d,
                    Carbohydrates = 0.1d,
                    Fat = 20.7d,
                    IsDefault = true,
                    IsAvailable = true,
                    DateAdded = DateTime.Now
                },
                new Product()
                {
                    Id = 7,
                    UserId = _adminUserGuid,
                    Name = "Passata - Sottile Gusto",
                    Kcal = 28.9d,
                    Protein = 1.7d,
                    Carbohydrates = 4.4d,
                    Fat = 0.5d,
                    IsDefault = true,
                    IsAvailable = true,
                    DateAdded = DateTime.Now
                },
                new Product()
                {
                    Id = 8,
                    UserId = _adminUserGuid,
                    Name = "Onion",
                    Kcal = 27.2d,
                    Protein = 1.2d,
                    Carbohydrates = 4.7d,
                    Fat = 0.4d,
                    IsDefault = true,
                    IsAvailable = false,
                    DateAdded = DateTime.Now
                },
                new Product()
                {
                    Id = 9,
                    UserId = _adminUserGuid,
                    Name = "Beef",
                    Kcal = 107.8d,
                    Protein = 20.1d,
                    Carbohydrates = 0d,
                    Fat = 2.3d,
                    IsDefault = true,
                    IsAvailable = true,
                    DateAdded = DateTime.Now
                },
                new Product()
                {
                    Id = 10,
                    UserId = _userUserGuid,
                    Name = "Turkey",
                    Kcal = 123.0d,
                    Protein = 21.0d,
                    Carbohydrates = 0.0d,
                    Fat = 3.0d,
                    IsDefault = false,
                    IsAvailable = true,
                    DateAdded = DateTime.Now
                }
            );
        }

        private static void SeedMealTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meal>().HasData(
                new Meal()
                {
                    Id = 1,
                    Deleted = true,
                    UserId = _adminUserGuid,
                    MealName = "Initial meal"
                },
                new Meal()
                {
                    Id = 2,
                    DateEaten = DateTime.Now,
                    UserId = _adminUserGuid,
                    MealName = "Chicken stew"
                }
            );
        }

        private static void SeedMealLogTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MealLog>().HasData(
                new MealLog()
                {
                    Id = 1,
                    MealId = 1,
                    DateEaten = DateTime.Now,
                    UserId = _adminUserGuid,
                },
                new MealLog()
                {
                    Id = 2,
                    MealId = 1,
                    DateEaten = DateTime.Now.AddDays(-1),
                    UserId = _adminUserGuid,
                },
                new MealLog()
                {
                    Id = 3,
                    MealId = 2,
                    DateEaten = DateTime.Now.AddDays(-2),
                    UserId = _adminUserGuid,
                }
            );
        }

        private static void SeedMealProductTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MealProduct>().HasData(
                new MealProduct()
                {
                    Id = 1,
                    MealId = 1,
                    ProductId = 1,
                    Weight = 200.0d
                }, new MealProduct()
                {
                    Id = 2,
                    MealId = 1,
                    ProductId = 2,
                    Weight = 60.0d
                }, new MealProduct()
                {
                    Id = 3,
                    MealId = 1,
                    ProductId = 4,
                    Weight = 35.0d
                },
                new MealProduct()
                {
                    Id = 4,
                    MealId = 2,
                    ProductId = 1,
                    Weight = 132.0d
                }, new MealProduct()
                {
                    Id = 5,
                    MealId = 2,
                    ProductId = 2,
                    Weight = 250.0d
                }, new MealProduct()
                {
                    Id = 6,
                    MealId = 2,
                    ProductId = 4,
                    Weight = 95.0d
                }
            );
        }
    }
}
