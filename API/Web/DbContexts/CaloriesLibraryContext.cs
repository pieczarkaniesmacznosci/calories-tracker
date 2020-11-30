
using System;
using API.Web.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Web.DbContexts
{
    public class CaloriesLibraryContext : IdentityDbContext<User, Role, int>
    {
        public CaloriesLibraryContext(DbContextOptions<CaloriesLibraryContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<UserNutrition> UserNutritions { get; set; }
        public DbSet<UserWeight> UserWeights { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MealProduct>().HasKey(mp => new { mp.MealId, mp.ProductId });

            modelBuilder.Entity<User>();
            modelBuilder.Entity<User>().HasMany(x => x.UserNutritions).WithOne(x => x.User);
            modelBuilder.Entity<User>().HasMany(x => x.UserWeights).WithOne(x => x.User);

            modelBuilder.Entity<Role>();
            
            SeedUserTable(modelBuilder);
            SeedProductTable(modelBuilder);
            SeedMealTable(modelBuilder);
            SeedMealProductTable(modelBuilder);
            SeedUserNutritionTable(modelBuilder);
            SeedUserWeightTable(modelBuilder);
        }

        private void SeedUserTable(ModelBuilder modelBuilder)
        {
            var user = new User
            {
                Id = 1,
                FirstName = "First",
                LastName = "Last",
                Email = "email@domain.com",
                UserName = "email@domain.com",
                NormalizedUserName = "EMAIL@DOMAIN.COM",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            PasswordHasher<User> ph = new PasswordHasher<User>();
            user.PasswordHash = ph.HashPassword(user, "support");

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin", NormalizedName = "ADMIN", Description = "Administration role" }
            );

            modelBuilder.Entity<User>().HasData(user);
        }

        private void SeedUserWeightTable(ModelBuilder modelBuilder)
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

        private void SeedUserNutritionTable(ModelBuilder modelBuilder)
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

        private void SeedProductTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                            new Product()
                            {
                                Id = 1,
                                Name = "Chicken",
                                Kcal = 111.0d,
                                Protein = 21.0d,
                                Carbohydrates = 0.0d,
                                Fat = 3.0d,
                            },
                            new Product()
                            {
                                Id = 2,
                                Name = "Rice",
                                Kcal = 339.5d,
                                Protein = 6.7d,
                                Carbohydrates = 76.6d,
                                Fat = 0.7d,
                            },
                            new Product()
                            {
                                Id = 3,
                                Name = "Milky Chocolate",
                                Kcal = 580.8d,
                                Protein = 7.7d,
                                Carbohydrates = 51.1d,
                                Fat = 38.4d,
                            },
                            new Product()
                            {
                                Id = 4,
                                Name = "White bread",
                                Kcal = 237.7d,
                                Protein = 4.0d,
                                Carbohydrates = 52.5d,
                                Fat = 1.3d,
                            }
                        );
        }

        private void SeedMealTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meal>().HasData(
                            new Meal()
                            {
                                Id = 1,
                                DateEaten = DateTime.Now,
                                UserId = 1,
                            },
                            new Meal()
                            {
                                Id = 2,
                                DateEaten = DateTime.Now,
                                UserId = 1,
                                IsSaved = true,
                                MealName = "Chicken stew"
                            }
                        );
        }

        private void SeedMealProductTable(ModelBuilder modelBuilder)
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
                                Id = 1,
                                MealId = 2,
                                ProductId = 1,
                                Weight = 132.0d
                            }, new MealProduct()
                            {
                                Id = 2,
                                MealId = 2,
                                ProductId = 2,
                                Weight = 250.0d
                            }, new MealProduct()
                            {
                                Id = 3,
                                MealId = 2,
                                ProductId = 4,
                                Weight = 95.0d
                            }
                        );
        }
    }
}