using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class teste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(60)",
                oldMaxLength: 60);

            migrationBuilder.AddColumn<Guid>(
                name: "SaleEntityId",
                table: "SoldProducts",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PurchaseEntityId",
                table: "PurchasedProducts",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SoldProducts_SaleEntityId",
                table: "SoldProducts",
                column: "SaleEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedProducts_PurchaseEntityId",
                table: "PurchasedProducts",
                column: "PurchaseEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasedProducts_Purchases_PurchaseEntityId",
                table: "PurchasedProducts",
                column: "PurchaseEntityId",
                principalTable: "Purchases",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SoldProducts_Sales_SaleEntityId",
                table: "SoldProducts",
                column: "SaleEntityId",
                principalTable: "Sales",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchasedProducts_Purchases_PurchaseEntityId",
                table: "PurchasedProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_SoldProducts_Sales_SaleEntityId",
                table: "SoldProducts");

            migrationBuilder.DropIndex(
                name: "IX_SoldProducts_SaleEntityId",
                table: "SoldProducts");

            migrationBuilder.DropIndex(
                name: "IX_PurchasedProducts_PurchaseEntityId",
                table: "PurchasedProducts");

            migrationBuilder.DropColumn(
                name: "SaleEntityId",
                table: "SoldProducts");

            migrationBuilder.DropColumn(
                name: "PurchaseEntityId",
                table: "PurchasedProducts");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "character varying(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
