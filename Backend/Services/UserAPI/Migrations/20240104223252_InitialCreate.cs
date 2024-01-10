using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserNutrition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Kcal = table.Column<double>(type: "float", nullable: false),
                    Protein = table.Column<double>(type: "float", nullable: false),
                    Carbohydrates = table.Column<double>(type: "float", nullable: false),
                    Fat = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNutrition", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserWeight",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWeight", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "UserNutrition",
                columns: new[] { "Id", "Carbohydrates", "Date", "Fat", "Kcal", "Protein", "UserId" },
                values: new object[] { 1, 246.0, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 57.509999999999998, 2070.0, 142.0, 1 });

            migrationBuilder.InsertData(
                table: "UserWeight",
                columns: new[] { "Id", "Date", "UserId", "Weight" },
                values: new object[] { 1, new DateTime(2020, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 71.5 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserNutrition");

            migrationBuilder.DropTable(
                name: "UserWeight");
        }
    }
}
