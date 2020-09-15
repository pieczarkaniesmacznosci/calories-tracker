using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompositionId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Weight = table.Column<double>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: false),
                    Kcal = table.Column<double>(nullable: false),
                    Protein = table.Column<double>(nullable: false),
                    Carbohydrates = table.Column<double>(nullable: false),
                    Fat = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Carbohydrates", "Fat", "Kcal", "Name", "Protein" },
                values: new object[] { 1, 0.0, 3.0, 111.0, "Chicken", 21.0 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Carbohydrates", "Fat", "Kcal", "Name", "Protein" },
                values: new object[] { 2, 76.599999999999994, 0.69999999999999996, 339.5, "Rice", 6.7000000000000002 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Carbohydrates", "Fat", "Kcal", "Name", "Protein" },
                values: new object[] { 3, 51.100000000000001, 38.399999999999999, 580.79999999999995, "Milky Chocolate", 7.7000000000000002 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Carbohydrates", "Fat", "Kcal", "Name", "Protein" },
                values: new object[] { 4, 52.5, 1.3, 237.69999999999999, "White bread", 4.0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
