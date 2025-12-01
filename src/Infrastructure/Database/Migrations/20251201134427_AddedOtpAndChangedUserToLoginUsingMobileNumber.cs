using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations;

    /// <inheritdoc />
    public partial class AddedOtpAndChangedUserToLoginUsingMobileNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                schema: "public",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "public",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                schema: "public",
                table: "Users",
                newName: "MobileNumber");

            migrationBuilder.CreateTable(
                name: "OtpStores",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MobileNumber = table.Column<string>(type: "text", nullable: false),
                    Otp = table.Column<string>(type: "text", nullable: false),
                    Expiry = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table => table.PrimaryKey("PK_OtpStores", x => x.Id));

            migrationBuilder.CreateIndex(
                name: "IX_Users_MobileNumber",
                schema: "public",
                table: "Users",
                column: "MobileNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OtpStores",
                schema: "public");

            migrationBuilder.DropIndex(
                name: "IX_Users_MobileNumber",
                schema: "public",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "MobileNumber",
                schema: "public",
                table: "Users",
                newName: "PasswordHash");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "public",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                schema: "public",
                table: "Users",
                column: "Email",
                unique: true);
        }
    }

