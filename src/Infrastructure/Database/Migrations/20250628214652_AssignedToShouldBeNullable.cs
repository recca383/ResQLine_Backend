using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class AssignedToShouldBeNullable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_TodoItems_Users_AssignedTo",
            schema: "public",
            table: "TodoItems");

        migrationBuilder.AlterColumn<Guid>(
            name: "AssignedTo",
            schema: "public",
            table: "TodoItems",
            type: "uuid",
            nullable: true,
            oldClrType: typeof(Guid),
            oldType: "uuid");

        migrationBuilder.AddForeignKey(
            name: "FK_TodoItems_Users_AssignedTo",
            schema: "public",
            table: "TodoItems",
            column: "AssignedTo",
            principalSchema: "public",
            principalTable: "Users",
            principalColumn: "Id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_TodoItems_Users_AssignedTo",
            schema: "public",
            table: "TodoItems");

        migrationBuilder.AlterColumn<Guid>(
            name: "AssignedTo",
            schema: "public",
            table: "TodoItems",
            type: "uuid",
            nullable: false,
            defaultValue: Guid.Empty,
            oldClrType: typeof(Guid),
            oldType: "uuid",
            oldNullable: true);

        migrationBuilder.AddForeignKey(
            name: "FK_TodoItems_Users_AssignedTo",
            schema: "public",
            table: "TodoItems",
            column: "AssignedTo",
            principalSchema: "public",
            principalTable: "Users",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
