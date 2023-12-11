using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class IsSavedChangedToDeletedInMealTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsSaved",
                table: "Meals",
                newName: "Deleted");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MealName",
                table: "Meals",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "c94d2952-4ee8-42dc-9ba1-a9830660a9eb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "556bd23b-a44f-46c2-a35c-940b667bcb9e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "838a721f-d0d7-4f1d-b829-c15038202980", "AQAAAAEAACcQAAAAENMmuLCkiG4fCv81QGc46Yfs2QmwiwY6CqDLZGNsKd50m44ZjbxbRywCD3eazqrx0A==", "028b0f6e-7669-4a6f-be51-206d65383b18" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c11f4dad-e50e-4f2c-b5db-1ac5e1fbaf04", "AQAAAAEAACcQAAAAEKc1ZzlDK2sVZUOV9AvApXPVgJLR/RH7CBHtqHStmtUGd19UM73Y22nmR6UnLTbzIQ==", "1b7172e6-7ab6-4989-bc23-8f36780451e4" });

            migrationBuilder.UpdateData(
                table: "MealLogs",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateEaten",
                value: new DateTime(2023, 12, 11, 23, 9, 50, 614, DateTimeKind.Local).AddTicks(5802));

            migrationBuilder.UpdateData(
                table: "MealLogs",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateEaten",
                value: new DateTime(2023, 12, 10, 23, 9, 50, 614, DateTimeKind.Local).AddTicks(5804));

            migrationBuilder.UpdateData(
                table: "MealLogs",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateEaten",
                value: new DateTime(2023, 12, 9, 23, 9, 50, 614, DateTimeKind.Local).AddTicks(5806));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 1,
                column: "Deleted",
                value: true);

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateEaten", "Deleted" },
                values: new object[] { new DateTime(2023, 12, 11, 23, 9, 50, 614, DateTimeKind.Local).AddTicks(5788), false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2023, 12, 11, 23, 9, 50, 614, DateTimeKind.Local).AddTicks(5722));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2023, 12, 11, 23, 9, 50, 614, DateTimeKind.Local).AddTicks(5757));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2023, 12, 11, 23, 9, 50, 614, DateTimeKind.Local).AddTicks(5759));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateAdded",
                value: new DateTime(2023, 12, 11, 23, 9, 50, 614, DateTimeKind.Local).AddTicks(5761));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateAdded",
                value: new DateTime(2023, 12, 11, 23, 9, 50, 614, DateTimeKind.Local).AddTicks(5762));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateAdded",
                value: new DateTime(2023, 12, 11, 23, 9, 50, 614, DateTimeKind.Local).AddTicks(5764));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateAdded",
                value: new DateTime(2023, 12, 11, 23, 9, 50, 614, DateTimeKind.Local).AddTicks(5765));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateAdded",
                value: new DateTime(2023, 12, 11, 23, 9, 50, 614, DateTimeKind.Local).AddTicks(5767));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateAdded",
                value: new DateTime(2023, 12, 11, 23, 9, 50, 614, DateTimeKind.Local).AddTicks(5768));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateAdded",
                value: new DateTime(2023, 12, 11, 23, 9, 50, 614, DateTimeKind.Local).AddTicks(5770));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Deleted",
                table: "Meals",
                newName: "IsSaved");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MealName",
                table: "Meals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "0e501356-d541-4046-a68b-4b69e54efdd6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "fa4c858e-43fe-44cc-b839-7ed95380cc4d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9704633c-83f1-445c-a121-64172c1cdfd5", "AQAAAAEAACcQAAAAEOlRiDLN/J1Lxjp+MZhw2P3yID9I6YJUFCe+GZiaXFh203QZ/i9rjT7ZtDLATYARqg==", "20d35364-f1e9-482e-b431-159a120d6362" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "45fde987-15bc-4ee4-a5e4-44946260923a", "AQAAAAEAACcQAAAAEEtb3P8WGbjT6+h/NF2LFyCg+o7Bj7jbuTQc0xCbgYpzLJVBw0FxdN/6zGPBj0ht9A==", "c9806488-96b8-4245-95f2-23a196e32de4" });

            migrationBuilder.UpdateData(
                table: "MealLogs",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateEaten",
                value: new DateTime(2023, 11, 27, 22, 32, 2, 117, DateTimeKind.Local).AddTicks(5925));

            migrationBuilder.UpdateData(
                table: "MealLogs",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateEaten",
                value: new DateTime(2023, 11, 26, 22, 32, 2, 117, DateTimeKind.Local).AddTicks(5927));

            migrationBuilder.UpdateData(
                table: "MealLogs",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateEaten",
                value: new DateTime(2023, 11, 25, 22, 32, 2, 117, DateTimeKind.Local).AddTicks(5928));

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsSaved",
                value: false);

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateEaten", "IsSaved" },
                values: new object[] { new DateTime(2023, 11, 27, 22, 32, 2, 117, DateTimeKind.Local).AddTicks(5913), true });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2023, 11, 27, 22, 32, 2, 117, DateTimeKind.Local).AddTicks(5846));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2023, 11, 27, 22, 32, 2, 117, DateTimeKind.Local).AddTicks(5881));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2023, 11, 27, 22, 32, 2, 117, DateTimeKind.Local).AddTicks(5883));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateAdded",
                value: new DateTime(2023, 11, 27, 22, 32, 2, 117, DateTimeKind.Local).AddTicks(5884));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateAdded",
                value: new DateTime(2023, 11, 27, 22, 32, 2, 117, DateTimeKind.Local).AddTicks(5886));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateAdded",
                value: new DateTime(2023, 11, 27, 22, 32, 2, 117, DateTimeKind.Local).AddTicks(5887));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateAdded",
                value: new DateTime(2023, 11, 27, 22, 32, 2, 117, DateTimeKind.Local).AddTicks(5889));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateAdded",
                value: new DateTime(2023, 11, 27, 22, 32, 2, 117, DateTimeKind.Local).AddTicks(5891));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateAdded",
                value: new DateTime(2023, 11, 27, 22, 32, 2, 117, DateTimeKind.Local).AddTicks(5893));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateAdded",
                value: new DateTime(2023, 11, 27, 22, 32, 2, 117, DateTimeKind.Local).AddTicks(5895));
        }
    }
}
