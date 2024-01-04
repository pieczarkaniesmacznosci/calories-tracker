﻿// <auto-generated />
using System;
using DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CaloriesAPI.Migrations
{
    [DbContext(typeof(CaloriesDbContext))]
    partial class CaloriesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entities.Meal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateEaten")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("MealName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Meals");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Deleted = true,
                            MealName = "Initial meal",
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            DateEaten = new DateTime(2024, 1, 4, 19, 10, 33, 918, DateTimeKind.Local).AddTicks(6101),
                            Deleted = false,
                            MealName = "Chicken stew",
                            UserId = 1
                        });
                });

            modelBuilder.Entity("Entities.MealLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateEaten")
                        .HasColumnType("datetime2");

                    b.Property<int>("MealId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MealId");

                    b.ToTable("MealLogs");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateEaten = new DateTime(2024, 1, 4, 19, 10, 33, 918, DateTimeKind.Local).AddTicks(6123),
                            MealId = 1,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            DateEaten = new DateTime(2024, 1, 3, 19, 10, 33, 918, DateTimeKind.Local).AddTicks(6126),
                            MealId = 1,
                            UserId = 1
                        },
                        new
                        {
                            Id = 3,
                            DateEaten = new DateTime(2024, 1, 2, 19, 10, 33, 918, DateTimeKind.Local).AddTicks(6129),
                            MealId = 2,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("Entities.MealProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MealId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("MealId");

                    b.HasIndex("ProductId");

                    b.ToTable("MealProduct");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            MealId = 1,
                            ProductId = 1,
                            Weight = 200.0
                        },
                        new
                        {
                            Id = 2,
                            MealId = 1,
                            ProductId = 2,
                            Weight = 60.0
                        },
                        new
                        {
                            Id = 3,
                            MealId = 1,
                            ProductId = 4,
                            Weight = 35.0
                        },
                        new
                        {
                            Id = 4,
                            MealId = 2,
                            ProductId = 1,
                            Weight = 132.0
                        },
                        new
                        {
                            Id = 5,
                            MealId = 2,
                            ProductId = 2,
                            Weight = 250.0
                        },
                        new
                        {
                            Id = 6,
                            MealId = 2,
                            ProductId = 4,
                            Weight = 95.0
                        });
                });

            modelBuilder.Entity("Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Carbohydrates")
                        .HasColumnType("float");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<double>("Fat")
                        .HasColumnType("float");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit");

                    b.Property<double>("Kcal")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Protein")
                        .HasColumnType("float");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Carbohydrates = 0.0,
                            DateAdded = new DateTime(2024, 1, 4, 19, 10, 33, 918, DateTimeKind.Local).AddTicks(5900),
                            Fat = 3.0,
                            IsAvailable = true,
                            IsDefault = true,
                            Kcal = 111.0,
                            Name = "Chicken",
                            Protein = 21.0,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            Carbohydrates = 76.599999999999994,
                            DateAdded = new DateTime(2024, 1, 4, 19, 10, 33, 918, DateTimeKind.Local).AddTicks(5948),
                            Fat = 0.69999999999999996,
                            IsAvailable = true,
                            IsDefault = true,
                            Kcal = 339.5,
                            Name = "Rice",
                            Protein = 6.7000000000000002,
                            UserId = 1
                        },
                        new
                        {
                            Id = 3,
                            Carbohydrates = 51.100000000000001,
                            DateAdded = new DateTime(2024, 1, 4, 19, 10, 33, 918, DateTimeKind.Local).AddTicks(5951),
                            Fat = 38.399999999999999,
                            IsAvailable = true,
                            IsDefault = true,
                            Kcal = 580.79999999999995,
                            Name = "Milky Chocolate",
                            Protein = 7.7000000000000002,
                            UserId = 1
                        },
                        new
                        {
                            Id = 4,
                            Carbohydrates = 52.5,
                            DateAdded = new DateTime(2024, 1, 4, 19, 10, 33, 918, DateTimeKind.Local).AddTicks(5953),
                            Fat = 1.3,
                            IsAvailable = true,
                            IsDefault = true,
                            Kcal = 237.69999999999999,
                            Name = "White bread",
                            Protein = 4.0,
                            UserId = 1
                        },
                        new
                        {
                            Id = 5,
                            Carbohydrates = 20.0,
                            DateAdded = new DateTime(2024, 1, 4, 19, 10, 33, 918, DateTimeKind.Local).AddTicks(5956),
                            Fat = 0.90000000000000002,
                            IsAvailable = true,
                            IsDefault = true,
                            Kcal = 94.5,
                            Name = "Tomato ketchup",
                            Protein = 1.6000000000000001,
                            UserId = 1
                        },
                        new
                        {
                            Id = 6,
                            Carbohydrates = 0.10000000000000001,
                            DateAdded = new DateTime(2024, 1, 4, 19, 10, 33, 918, DateTimeKind.Local).AddTicks(5958),
                            Fat = 20.699999999999999,
                            IsAvailable = true,
                            IsDefault = true,
                            Kcal = 288.69999999999999,
                            Name = "Gouda cheese",
                            Protein = 25.5,
                            UserId = 1
                        },
                        new
                        {
                            Id = 7,
                            Carbohydrates = 4.4000000000000004,
                            DateAdded = new DateTime(2024, 1, 4, 19, 10, 33, 918, DateTimeKind.Local).AddTicks(5961),
                            Fat = 0.5,
                            IsAvailable = true,
                            IsDefault = true,
                            Kcal = 28.899999999999999,
                            Name = "Passata - Sottile Gusto",
                            Protein = 1.7,
                            UserId = 1
                        },
                        new
                        {
                            Id = 8,
                            Carbohydrates = 4.7000000000000002,
                            DateAdded = new DateTime(2024, 1, 4, 19, 10, 33, 918, DateTimeKind.Local).AddTicks(5963),
                            Fat = 0.40000000000000002,
                            IsAvailable = false,
                            IsDefault = true,
                            Kcal = 27.199999999999999,
                            Name = "Onion",
                            Protein = 1.2,
                            UserId = 1
                        },
                        new
                        {
                            Id = 9,
                            Carbohydrates = 0.0,
                            DateAdded = new DateTime(2024, 1, 4, 19, 10, 33, 918, DateTimeKind.Local).AddTicks(5966),
                            Fat = 2.2999999999999998,
                            IsAvailable = true,
                            IsDefault = true,
                            Kcal = 107.8,
                            Name = "Beef",
                            Protein = 20.100000000000001,
                            UserId = 1
                        },
                        new
                        {
                            Id = 10,
                            Carbohydrates = 0.0,
                            DateAdded = new DateTime(2024, 1, 4, 19, 10, 33, 918, DateTimeKind.Local).AddTicks(5968),
                            Fat = 3.0,
                            IsAvailable = true,
                            IsDefault = false,
                            Kcal = 123.0,
                            Name = "Turkey",
                            Protein = 21.0,
                            UserId = 2
                        });
                });

            modelBuilder.Entity("Entities.MealLog", b =>
                {
                    b.HasOne("Entities.Meal", "Meal")
                        .WithMany("MealLogs")
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Meal");
                });

            modelBuilder.Entity("Entities.MealProduct", b =>
                {
                    b.HasOne("Entities.Meal", "Meal")
                        .WithMany("MealProducts")
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Meal");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Entities.Meal", b =>
                {
                    b.Navigation("MealLogs");

                    b.Navigation("MealProducts");
                });
#pragma warning restore 612, 618
        }
    }
}