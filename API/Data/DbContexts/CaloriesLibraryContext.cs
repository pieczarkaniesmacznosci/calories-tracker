using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data.DbContexts
{
    public class CaloriesLibraryContext : IdentityDbContext<User, Role, int>
    {
        private readonly IConfiguration _config;

        public CaloriesLibraryContext(DbContextOptions<CaloriesLibraryContext> options, IConfiguration configuration) : base(options)
        {
            _config = configuration;
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<MealLog> MealLogs { get; set; }
        public DbSet<UserNutrition> UserNutritions { get; set; }
        public DbSet<UserWeight> UserWeights { get; set; }

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
            modelBuilder.Entity<MealLog>().HasOne(x => x.User).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Meal>().HasMany(x => x.MealLogs);
            modelBuilder.Entity<Meal>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>();
            modelBuilder.Entity<User>().HasMany(x => x.UserNutritions).WithOne(x => x.User);
            modelBuilder.Entity<User>().HasMany(x => x.UserWeights).WithOne(x => x.User);
            modelBuilder.Entity<User>().HasMany(x => x.MealLogs).WithOne(x => x.User);
            modelBuilder.Entity<Role>();

            SeedUserTable(modelBuilder);
            SeedProductTable(modelBuilder);
            SeedMealTable(modelBuilder);
            SeedMealLogTable(modelBuilder);
            SeedMealProductTable(modelBuilder);
            SeedUserNutritionTable(modelBuilder);
            SeedUserWeightTable(modelBuilder);
        }

        private void SeedUserTable(ModelBuilder modelBuilder)
        {
            var user = new User
            {
                Id = 1,
                FirstName = "Mike",
                LastName = "Smith",
                Email = _config["AdminName"],
                UserName = _config["AdminName"],
                NormalizedUserName = _config["AdminName"].ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var regularUser = new User
            {
                Id = 2,
                FirstName = "Dave",
                LastName = "Murray",
                Email = _config["RegularUserName"],
                UserName = _config["RegularUserName"],
                NormalizedUserName = _config["RegularUserName"].ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString()
            };

            PasswordHasher<User> ph = new();
            user.PasswordHash = ph.HashPassword(user, _config["AdminPassword"]);
            regularUser.PasswordHash = ph.HashPassword(regularUser, _config["RegularUserPassword"]);

            modelBuilder.Entity<User>().HasData(user, regularUser);

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin", NormalizedName = "ADMIN", Description = "Administration role" },
                new Role { Id = 2, Name = "User", NormalizedName = "USER", Description = "User role" }
            );

            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { Id = 1, RoleId = 1, UserId = 1 },
                new UserRole { Id = 2, RoleId = 2, UserId = 2 }
            );
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

        private static void SeedProductTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    Id = 1,
                    UserId = 1,
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
                    UserId = 1,
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
                    UserId = 1,
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
                    UserId = 1,
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
                    UserId = 1,
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
                    UserId = 1,
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
                    UserId = 1,
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
                    UserId = 1,
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
                    UserId = 1,
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
                    UserId = 2,
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
                    UserId = 1,
                    MealName = "Initial meal"
                },
                new Meal()
                {
                    Id = 2,
                    DateEaten = DateTime.Now,
                    UserId = 1,
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
                    UserId = 1,
                },
                new MealLog()
                {
                    Id = 2,
                    MealId = 1,
                    DateEaten = DateTime.Now.AddDays(-1),
                    UserId = 1,
                },
                new MealLog()
                {
                    Id = 3,
                    MealId = 2,
                    DateEaten = DateTime.Now.AddDays(-2),
                    UserId = 1,
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
