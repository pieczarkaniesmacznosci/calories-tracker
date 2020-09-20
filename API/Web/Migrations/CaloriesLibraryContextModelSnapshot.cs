﻿// <auto-generated />
using System;
using API.Web.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Web.Migrations
{
    [DbContext(typeof(CaloriesLibraryContext))]
    partial class CaloriesLibraryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5");

            modelBuilder.Entity("API.Web.Entities.Meal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<double>("Weight")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Meals");
                });

            modelBuilder.Entity("API.Web.Entities.MealElement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MealId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Weight")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("MealId");

                    b.HasIndex("ProductId");

                    b.ToTable("MealElements");
                });

            modelBuilder.Entity("API.Web.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Carbohydrates")
                        .HasColumnType("REAL");

                    b.Property<double>("Fat")
                        .HasColumnType("REAL");

                    b.Property<double>("Kcal")
                        .HasColumnType("REAL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Protein")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Carbohydrates = 0.0,
                            Fat = 3.0,
                            Kcal = 111.0,
                            Name = "Chicken",
                            Protein = 21.0
                        },
                        new
                        {
                            Id = 2,
                            Carbohydrates = 76.599999999999994,
                            Fat = 0.69999999999999996,
                            Kcal = 339.5,
                            Name = "Rice",
                            Protein = 6.7000000000000002
                        },
                        new
                        {
                            Id = 3,
                            Carbohydrates = 51.100000000000001,
                            Fat = 38.399999999999999,
                            Kcal = 580.79999999999995,
                            Name = "Milky Chocolate",
                            Protein = 7.7000000000000002
                        },
                        new
                        {
                            Id = 4,
                            Carbohydrates = 52.5,
                            Fat = 1.3,
                            Kcal = 237.69999999999999,
                            Name = "White bread",
                            Protein = 4.0
                        });
                });

            modelBuilder.Entity("API.Web.Entities.MealElement", b =>
                {
                    b.HasOne("API.Web.Entities.Meal", null)
                        .WithMany("MealElements")
                        .HasForeignKey("MealId");

                    b.HasOne("API.Web.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
