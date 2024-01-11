using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CaloriesAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    MealName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateEaten = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kcal = table.Column<double>(type: "float", nullable: false),
                    Protein = table.Column<double>(type: "float", nullable: false),
                    Carbohydrates = table.Column<double>(type: "float", nullable: false),
                    Fat = table.Column<double>(type: "float", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MealLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MealId = table.Column<int>(type: "int", nullable: false),
                    DateEaten = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealLogs_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealProduct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    MealId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealProduct_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealProduct_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Meals",
                columns: new[] { "Id", "DateEaten", "Deleted", "MealName", "UserId" },
                values: new object[,]
                {
                    { 1, null, true, "Initial meal", new Guid("10000000-0000-0000-0000-000000000000") },
                    { 2, new DateTime(2024, 1, 11, 19, 12, 6, 294, DateTimeKind.Local).AddTicks(214), false, "Chicken stew", new Guid("10000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Carbohydrates", "DateAdded", "Fat", "IsAvailable", "IsDefault", "Kcal", "Name", "Protein", "UserId" },
                values: new object[,]
                {
                    { 1, 0.0, new DateTime(2024, 1, 11, 19, 12, 6, 294, DateTimeKind.Local).AddTicks(53), 3.0, true, true, 111.0, "Chicken", 21.0, new Guid("10000000-0000-0000-0000-000000000000") },
                    { 2, 76.599999999999994, new DateTime(2024, 1, 11, 19, 12, 6, 294, DateTimeKind.Local).AddTicks(102), 0.69999999999999996, true, true, 339.5, "Rice", 6.7000000000000002, new Guid("10000000-0000-0000-0000-000000000000") },
                    { 3, 51.100000000000001, new DateTime(2024, 1, 11, 19, 12, 6, 294, DateTimeKind.Local).AddTicks(105), 38.399999999999999, true, true, 580.79999999999995, "Milky Chocolate", 7.7000000000000002, new Guid("10000000-0000-0000-0000-000000000000") },
                    { 4, 52.5, new DateTime(2024, 1, 11, 19, 12, 6, 294, DateTimeKind.Local).AddTicks(107), 1.3, true, true, 237.69999999999999, "White bread", 4.0, new Guid("10000000-0000-0000-0000-000000000000") },
                    { 5, 20.0, new DateTime(2024, 1, 11, 19, 12, 6, 294, DateTimeKind.Local).AddTicks(110), 0.90000000000000002, true, true, 94.5, "Tomato ketchup", 1.6000000000000001, new Guid("10000000-0000-0000-0000-000000000000") },
                    { 6, 0.10000000000000001, new DateTime(2024, 1, 11, 19, 12, 6, 294, DateTimeKind.Local).AddTicks(112), 20.699999999999999, true, true, 288.69999999999999, "Gouda cheese", 25.5, new Guid("10000000-0000-0000-0000-000000000000") },
                    { 7, 4.4000000000000004, new DateTime(2024, 1, 11, 19, 12, 6, 294, DateTimeKind.Local).AddTicks(114), 0.5, true, true, 28.899999999999999, "Passata - Sottile Gusto", 1.7, new Guid("10000000-0000-0000-0000-000000000000") },
                    { 8, 4.7000000000000002, new DateTime(2024, 1, 11, 19, 12, 6, 294, DateTimeKind.Local).AddTicks(116), 0.40000000000000002, false, true, 27.199999999999999, "Onion", 1.2, new Guid("10000000-0000-0000-0000-000000000000") },
                    { 9, 0.0, new DateTime(2024, 1, 11, 19, 12, 6, 294, DateTimeKind.Local).AddTicks(118), 2.2999999999999998, true, true, 107.8, "Beef", 20.100000000000001, new Guid("10000000-0000-0000-0000-000000000000") },
                    { 10, 0.0, new DateTime(2024, 1, 11, 19, 12, 6, 294, DateTimeKind.Local).AddTicks(120), 3.0, true, false, 123.0, "Turkey", 21.0, new Guid("20000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "MealLogs",
                columns: new[] { "Id", "DateEaten", "MealId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 11, 19, 12, 6, 294, DateTimeKind.Local).AddTicks(234), 1, new Guid("10000000-0000-0000-0000-000000000000") },
                    { 2, new DateTime(2024, 1, 10, 19, 12, 6, 294, DateTimeKind.Local).AddTicks(238), 1, new Guid("10000000-0000-0000-0000-000000000000") },
                    { 3, new DateTime(2024, 1, 9, 19, 12, 6, 294, DateTimeKind.Local).AddTicks(241), 2, new Guid("10000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "MealProduct",
                columns: new[] { "Id", "MealId", "ProductId", "Weight" },
                values: new object[,]
                {
                    { 1, 1, 1, 200.0 },
                    { 2, 1, 2, 60.0 },
                    { 3, 1, 4, 35.0 },
                    { 4, 2, 1, 132.0 },
                    { 5, 2, 2, 250.0 },
                    { 6, 2, 4, 95.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MealLogs_MealId",
                table: "MealLogs",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_MealProduct_MealId",
                table: "MealProduct",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_MealProduct_ProductId",
                table: "MealProduct",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MealLogs");

            migrationBuilder.DropTable(
                name: "MealProduct");

            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
