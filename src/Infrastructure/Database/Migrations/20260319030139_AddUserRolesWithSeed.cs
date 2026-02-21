using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class AddUserRolesWithSeed : Migration
{
    private static readonly string[] columns = new[] { "Id", "Name" };

    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<Guid>(
            name: "RoleId",
            schema: "public",
            table: "Users",
            type: "uuid",
            nullable: false,
            defaultValue: Guid.Parse("e6b7f08e-76a4-4e33-aac0-bdd5909ad62d"));

        migrationBuilder.CreateTable(
            name: "Roles",
            schema: "public",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Name = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Roles", x => x.Id));

        migrationBuilder.InsertData(
            schema: "public",
            table: "Roles",
            columns: columns,
            values: new object[,]
            {
                { new Guid("6afd2278-f07f-44d3-86cd-f33d8f63dfae"), "Responder_BFP" },
                { new Guid("723a04b7-1a2e-49d5-a36f-089ffb740cb9"), "Responder_PNP" },
                { new Guid("c68b514c-9f37-44dd-ad4b-c7e9d58fbffd"), "Responder_CTMO" },
                { new Guid("ccb1ff2f-c363-4d80-9efa-050519b5be0c"), "Admin" },
                { new Guid("e6b7f08e-76a4-4e33-aac0-bdd5909ad62d"), "User" }
            });

        migrationBuilder.CreateIndex(
            name: "IX_Users_RoleId",
            schema: "public",
            table: "Users",
            column: "RoleId");

        migrationBuilder.AddForeignKey(
            name: "FK_Users_Roles_RoleId",
            schema: "public",
            table: "Users",
            column: "RoleId",
            principalSchema: "public",
            principalTable: "Roles",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Users_Roles_RoleId",
            schema: "public",
            table: "Users");

        migrationBuilder.DropTable(
            name: "Roles",
            schema: "public");

        migrationBuilder.DropIndex(
            name: "IX_Users_RoleId",
            schema: "public",
            table: "Users");

        migrationBuilder.DropColumn(
            name: "RoleId",
            schema: "public",
            table: "Users");
    }
}
