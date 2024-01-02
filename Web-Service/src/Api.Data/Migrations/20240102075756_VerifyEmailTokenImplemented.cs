using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class VerifyEmailTokenImplemented : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VerifiedAt",
                table: "Users",
                newName: "EmailVerifiedAt");

            migrationBuilder.RenameColumn(
                name: "VerificationToken",
                table: "Users",
                newName: "EmailVerificationToken");

            migrationBuilder.AddColumn<bool>(
                name: "EmailIsVerified",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailIsVerified",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "EmailVerifiedAt",
                table: "Users",
                newName: "VerifiedAt");

            migrationBuilder.RenameColumn(
                name: "EmailVerificationToken",
                table: "Users",
                newName: "VerificationToken");
        }
    }
}
