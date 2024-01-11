using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedUserSettingsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSettings_Users_Id",
                table: "UserSettings");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "UserSettings");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "UserSettings");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserSettings",
                newName: "UserEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSettings_Users_UserEntityId",
                table: "UserSettings",
                column: "UserEntityId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSettings_Users_UserEntityId",
                table: "UserSettings");

            migrationBuilder.RenameColumn(
                name: "UserEntityId",
                table: "UserSettings",
                newName: "Id");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "UserSettings",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "UserSettings",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSettings_Users_Id",
                table: "UserSettings",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
