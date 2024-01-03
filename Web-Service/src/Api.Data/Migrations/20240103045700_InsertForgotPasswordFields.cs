using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class InsertForgotPasswordFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ResetTokenExpires",
                table: "Users",
                newName: "ForgotPasswordExpires");

            migrationBuilder.RenameColumn(
                name: "PasswordResetToken",
                table: "Users",
                newName: "ForgotPasswordToken");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ForgotPasswordToken",
                table: "Users",
                newName: "PasswordResetToken");

            migrationBuilder.RenameColumn(
                name: "ForgotPasswordExpires",
                table: "Users",
                newName: "ResetTokenExpires");
        }
    }
}
