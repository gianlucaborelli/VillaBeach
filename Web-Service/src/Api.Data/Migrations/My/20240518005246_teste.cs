using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations.My
{
    /// <inheritdoc />
    public partial class teste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchasedProducts_Purchases_PurchaseEntityId",
                table: "PurchasedProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_SoldProducts_Sales_SaleEntityId",
                table: "SoldProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAddresses_Users_OwnerId",
                table: "UserAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserContacts_Users_OwnerId",
                table: "UserContacts");

            migrationBuilder.DropIndex(
                name: "IX_UserContacts_OwnerId",
                table: "UserContacts");

            migrationBuilder.DropIndex(
                name: "IX_UserAddresses_OwnerId",
                table: "UserAddresses");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "UserContacts");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "UserContacts");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "UserContacts");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "UserAddresses");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "UserAddresses");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "UserAddresses");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Tuitions");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Tuitions");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "SoldProducts");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "SoldProducts");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "PurchasedProducts");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "PurchasedProducts");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "ProductPrices");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "ProductPrices");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "PlanPrices");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "PlanPrices");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Enrollments");

            migrationBuilder.RenameColumn(
                name: "SaleEntityId",
                table: "SoldProducts",
                newName: "SaleId");

            migrationBuilder.RenameIndex(
                name: "IX_SoldProducts_SaleEntityId",
                table: "SoldProducts",
                newName: "IX_SoldProducts_SaleId");

            migrationBuilder.RenameColumn(
                name: "PurchaseEntityId",
                table: "PurchasedProducts",
                newName: "PurchaseId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchasedProducts_PurchaseEntityId",
                table: "PurchasedProducts",
                newName: "IX_PurchasedProducts_PurchaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasedProducts_Purchases_PurchaseId",
                table: "PurchasedProducts",
                column: "PurchaseId",
                principalTable: "Purchases",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SoldProducts_Sales_SaleId",
                table: "SoldProducts",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddresses_Users_Id",
                table: "UserAddresses",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserContacts_Users_Id",
                table: "UserContacts",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchasedProducts_Purchases_PurchaseId",
                table: "PurchasedProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_SoldProducts_Sales_SaleId",
                table: "SoldProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAddresses_Users_Id",
                table: "UserAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserContacts_Users_Id",
                table: "UserContacts");

            migrationBuilder.RenameColumn(
                name: "SaleId",
                table: "SoldProducts",
                newName: "SaleEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_SoldProducts_SaleId",
                table: "SoldProducts",
                newName: "IX_SoldProducts_SaleEntityId");

            migrationBuilder.RenameColumn(
                name: "PurchaseId",
                table: "PurchasedProducts",
                newName: "PurchaseEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchasedProducts_PurchaseId",
                table: "PurchasedProducts",
                newName: "IX_PurchasedProducts_PurchaseEntityId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "UserContacts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "UserContacts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "UserContacts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "UserAddresses",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "UserAddresses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "UserAddresses",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Tuitions",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "Tuitions",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "SoldProducts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "SoldProducts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Sales",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "Sales",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Purchases",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "Purchases",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "PurchasedProducts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "PurchasedProducts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Products",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "Products",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "ProductPrices",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "ProductPrices",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Plans",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "Plans",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "PlanPrices",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "PlanPrices",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Enrollments",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "Enrollments",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserContacts_OwnerId",
                table: "UserContacts",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresses_OwnerId",
                table: "UserAddresses",
                column: "OwnerId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddresses_Users_OwnerId",
                table: "UserAddresses",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserContacts_Users_OwnerId",
                table: "UserContacts",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
