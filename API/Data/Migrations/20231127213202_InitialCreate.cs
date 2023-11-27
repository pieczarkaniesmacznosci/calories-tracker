using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsSaved = table.Column<bool>(type: "bit", nullable: false),
                    MealName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateEaten = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meals_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kcal = table.Column<double>(type: "float", nullable: false),
                    Protein = table.Column<double>(type: "float", nullable: false),
                    Carbohydrates = table.Column<double>(type: "float", nullable: false),
                    Fat = table.Column<double>(type: "float", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserNutritions",
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
                    table.PrimaryKey("PK_UserNutritions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserNutritions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserWeights",
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
                    table.PrimaryKey("PK_UserWeights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserWeights_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MealId = table.Column<int>(type: "int", nullable: false),
                    DateEaten = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealLogs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
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
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "0e501356-d541-4046-a68b-4b69e54efdd6", "Administration role", "Admin", "ADMIN" },
                    { 2, "fa4c858e-43fe-44cc-b839-7ed95380cc4d", "User role", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "9704633c-83f1-445c-a121-64172c1cdfd5", "admin@tracly.com", false, "Mike", "Smith", false, null, null, "ADMIN@TRACLY.COM", "AQAAAAEAACcQAAAAEOlRiDLN/J1Lxjp+MZhw2P3yID9I6YJUFCe+GZiaXFh203QZ/i9rjT7ZtDLATYARqg==", null, false, "20d35364-f1e9-482e-b431-159a120d6362", false, "admin@tracly.com" },
                    { 2, 0, "45fde987-15bc-4ee4-a5e4-44946260923a", "user@tracly.com", false, "Dave", "Murray", false, null, null, "USER@TRACLY.COM", "AQAAAAEAACcQAAAAEEtb3P8WGbjT6+h/NF2LFyCg+o7Bj7jbuTQc0xCbgYpzLJVBw0FxdN/6zGPBj0ht9A==", null, false, "c9806488-96b8-4245-95f2-23a196e32de4", false, "user@tracly.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId", "Discriminator", "Id" },
                values: new object[,]
                {
                    { 1, 1, "UserRole", 1 },
                    { 2, 2, "UserRole", 2 }
                });

            migrationBuilder.InsertData(
                table: "Meals",
                columns: new[] { "Id", "DateEaten", "IsSaved", "MealName", "UserId" },
                values: new object[,]
                {
                    { 1, null, false, "Initial meal", 1 },
                    { 2, new DateTime(2023, 11, 27, 22, 32, 2, 117, DateTimeKind.Local).AddTicks(5913), true, "Chicken stew", 1 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Carbohydrates", "DateAdded", "Fat", "IsAvailable", "IsDefault", "Kcal", "Name", "Protein", "UserId" },
                values: new object[,]
                {
                    { 1, 0.0, new DateTime(2023, 11, 27, 22, 32, 2, 117, DateTimeKind.Local).AddTicks(5846), 3.0, true, true, 111.0, "Chicken", 21.0, 1 },
                    { 2, 76.599999999999994, new DateTime(2023, 11, 27, 22, 32, 2, 117, DateTimeKind.Local).AddTicks(5881), 0.69999999999999996, true, true, 339.5, "Rice", 6.7000000000000002, 1 },
                    { 3, 51.100000000000001, new DateTime(2023, 11, 27, 22, 32, 2, 117, DateTimeKind.Local).AddTicks(5883), 38.399999999999999, true, true, 580.79999999999995, "Milky Chocolate", 7.7000000000000002, 1 },
                    { 4, 52.5, new DateTime(2023, 11, 27, 22, 32, 2, 117, DateTimeKind.Local).AddTicks(5884), 1.3, true, true, 237.69999999999999, "White bread", 4.0, 1 },
                    { 5, 20.0, new DateTime(2023, 11, 27, 22, 32, 2, 117, DateTimeKind.Local).AddTicks(5886), 0.90000000000000002, true, true, 94.5, "Tomato ketchup", 1.6000000000000001, 1 },
                    { 6, 0.10000000000000001, new DateTime(2023, 11, 27, 22, 32, 2, 117, DateTimeKind.Local).AddTicks(5887), 20.699999999999999, true, true, 288.69999999999999, "Gouda cheese", 25.5, 1 },
                    { 7, 4.4000000000000004, new DateTime(2023, 11, 27, 22, 32, 2, 117, DateTimeKind.Local).AddTicks(5889), 0.5, true, true, 28.899999999999999, "Passata - Sottile Gusto", 1.7, 1 },
                    { 8, 4.7000000000000002, new DateTime(2023, 11, 27, 22, 32, 2, 117, DateTimeKind.Local).AddTicks(5891), 0.40000000000000002, false, true, 27.199999999999999, "Onion", 1.2, 1 },
                    { 9, 0.0, new DateTime(2023, 11, 27, 22, 32, 2, 117, DateTimeKind.Local).AddTicks(5893), 2.2999999999999998, true, true, 107.8, "Beef", 20.100000000000001, 1 },
                    { 10, 0.0, new DateTime(2023, 11, 27, 22, 32, 2, 117, DateTimeKind.Local).AddTicks(5895), 3.0, true, false, 123.0, "Turkey", 21.0, 2 }
                });

            migrationBuilder.InsertData(
                table: "UserNutritions",
                columns: new[] { "Id", "Carbohydrates", "Date", "Fat", "Kcal", "Protein", "UserId" },
                values: new object[] { 1, 246.0, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 57.509999999999998, 2070.0, 142.0, 1 });

            migrationBuilder.InsertData(
                table: "UserWeights",
                columns: new[] { "Id", "Date", "UserId", "Weight" },
                values: new object[] { 1, new DateTime(2020, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 71.5 });

            migrationBuilder.InsertData(
                table: "MealLogs",
                columns: new[] { "Id", "DateEaten", "MealId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 11, 27, 22, 32, 2, 117, DateTimeKind.Local).AddTicks(5925), 1, 1 },
                    { 2, new DateTime(2023, 11, 26, 22, 32, 2, 117, DateTimeKind.Local).AddTicks(5927), 1, 1 },
                    { 3, new DateTime(2023, 11, 25, 22, 32, 2, 117, DateTimeKind.Local).AddTicks(5928), 2, 1 }
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
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MealLogs_MealId",
                table: "MealLogs",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_MealLogs_UserId",
                table: "MealLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MealProduct_MealId",
                table: "MealProduct",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_MealProduct_ProductId",
                table: "MealProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_UserId",
                table: "Meals",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserId",
                table: "Products",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNutritions_UserId",
                table: "UserNutritions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWeights_UserId",
                table: "UserWeights",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "MealLogs");

            migrationBuilder.DropTable(
                name: "MealProduct");

            migrationBuilder.DropTable(
                name: "UserNutritions");

            migrationBuilder.DropTable(
                name: "UserWeights");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
