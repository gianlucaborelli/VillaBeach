using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Crosscutting.Identity.Migrations
{
    /// <inheritdoc />
    public partial class complementedNameOnIdentityUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("07900d8d-6c82-473f-9372-ee29e7e1b706"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e1e38f15-e906-49db-8572-999b212adf63"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ec08966e-2056-467d-b276-b349a3f50f4d"));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("16d13643-21b6-4d5b-a4fa-e4b3213015b4"), "3", "User", "User" },
                    { new Guid("4b1e51e4-5dff-41f8-82bd-49b80bbb771f"), "1", "SuperAdmin", "SuperAdmin" },
                    { new Guid("6c1bd4a4-6cb3-41da-acc5-265281ec356a"), "2", "Admin", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("16d13643-21b6-4d5b-a4fa-e4b3213015b4"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4b1e51e4-5dff-41f8-82bd-49b80bbb771f"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6c1bd4a4-6cb3-41da-acc5-265281ec356a"));

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("07900d8d-6c82-473f-9372-ee29e7e1b706"), "1", "SuperAdmin", "SuperAdmin" },
                    { new Guid("e1e38f15-e906-49db-8572-999b212adf63"), "3", "User", "User" },
                    { new Guid("ec08966e-2056-467d-b276-b349a3f50f4d"), "2", "Admin", "Admin" }
                });
        }
    }
}
