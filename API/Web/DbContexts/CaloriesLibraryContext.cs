
using System;
using System.Threading.Tasks;
using API.Web.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Web.DbContexts
{
    public class CaloriesLibraryContext : IdentityDbContext<User>
    {
        private readonly UserManager<User> _usermanager;

        public CaloriesLibraryContext(DbContextOptions<CaloriesLibraryContext> options, UserManager<User> usermanager) : base(options)
        {
            _usermanager = usermanager;
            Database.Migrate();
        }

        public DbSet<Product> Products {get; set;}
        public DbSet<Meal> Meals {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MealProduct>().HasKey(mp => new { mp.MealId, mp.ProductId });
            
            PopulateUserTableAsync(modelBuilder).Wait();
            PopulateProductTable(modelBuilder);
            PopulateMealTable(modelBuilder);
            PopulateMealProductTable(modelBuilder);
        }

        private async Task PopulateUserTableAsync(ModelBuilder modelBuilder)
        {
            var user = await _usermanager.FindByEmailAsync("email@domain.com");

            if(user == null)
            {
                user = new User()
                {
                    FirstName = "First",
                    LastName = "Last",
                    Email = "email@domain.com",
                    UserName = "email@domain.com"
                };
            var result = await _usermanager.CreateAsync(user, "P@ssw0rd");
            
            if(!result.Succeeded)
            {
                throw new InvalidOperationException("Could not create new user");
            }
            }
        }

        private static void PopulateProductTable(ModelBuilder modelBuilder)
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
        private static void PopulateMealTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meal>().HasData(
                            new Meal()
                            {
                                Id = 1,
                                DateEaten = DateTime.Now
                            }
                        );
        }
        private static void PopulateMealProductTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MealProduct>().HasData(
                            new MealProduct()
                            {
                                MealId = 1,
                                ProductId = 1,
                                Weight = 200.0d 
                            },new MealProduct()
                            {
                                MealId = 1,
                                ProductId = 2,
                                Weight = 60.0d 
                            },new MealProduct()
                            {
                                MealId = 1,
                                ProductId = 4,
                                Weight = 35.0d 
                            }
                        );
        }

    }
}