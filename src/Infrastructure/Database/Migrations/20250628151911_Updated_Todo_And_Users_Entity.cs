using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Updated_Todo_And_Users_Entity : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_todo_items_users_user_id",
            schema: "public",
            table: "todo_items");

        migrationBuilder.DropPrimaryKey(
            name: "pk_users",
            schema: "public",
            table: "users");

        migrationBuilder.DropPrimaryKey(
            name: "pk_todo_items",
            schema: "public",
            table: "todo_items");

        migrationBuilder.RenameTable(
            name: "users",
            schema: "public",
            newName: "Users",
            newSchema: "public");

        migrationBuilder.RenameTable(
            name: "todo_items",
            schema: "public",
            newName: "TodoItems",
            newSchema: "public");

        migrationBuilder.RenameColumn(
            name: "email",
            schema: "public",
            table: "Users",
            newName: "Email");

        migrationBuilder.RenameColumn(
            name: "id",
            schema: "public",
            table: "Users",
            newName: "Id");

        migrationBuilder.RenameColumn(
            name: "password_hash",
            schema: "public",
            table: "Users",
            newName: "PasswordHash");

        migrationBuilder.RenameColumn(
            name: "last_name",
            schema: "public",
            table: "Users",
            newName: "LastName");

        migrationBuilder.RenameColumn(
            name: "first_name",
            schema: "public",
            table: "Users",
            newName: "FirstName");

        migrationBuilder.RenameIndex(
            name: "ix_users_email",
            schema: "public",
            table: "Users",
            newName: "IX_Users_Email");

        migrationBuilder.RenameColumn(
            name: "priority",
            schema: "public",
            table: "TodoItems",
            newName: "Priority");

        migrationBuilder.RenameColumn(
            name: "labels",
            schema: "public",
            table: "TodoItems",
            newName: "Labels");

        migrationBuilder.RenameColumn(
            name: "description",
            schema: "public",
            table: "TodoItems",
            newName: "Description");

        migrationBuilder.RenameColumn(
            name: "id",
            schema: "public",
            table: "TodoItems",
            newName: "Id");

        migrationBuilder.RenameColumn(
            name: "is_completed",
            schema: "public",
            table: "TodoItems",
            newName: "IsCompleted");

        migrationBuilder.RenameColumn(
            name: "due_date",
            schema: "public",
            table: "TodoItems",
            newName: "DueDate");

        migrationBuilder.RenameColumn(
            name: "created_at",
            schema: "public",
            table: "TodoItems",
            newName: "CreatedAt");

        migrationBuilder.RenameColumn(
            name: "completed_at",
            schema: "public",
            table: "TodoItems",
            newName: "CompletedAt");

        migrationBuilder.RenameColumn(
            name: "user_id",
            schema: "public",
            table: "TodoItems",
            newName: "RequestedBy");

        migrationBuilder.RenameIndex(
            name: "ix_todo_items_user_id",
            schema: "public",
            table: "TodoItems",
            newName: "IX_TodoItems_RequestedBy");

        migrationBuilder.AddColumn<int>(
            name: "Team",
            schema: "public",
            table: "Users",
            type: "integer",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<string>(
            name: "UserName",
            schema: "public",
            table: "Users",
            type: "text",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<Guid>(
            name: "AssignedTo",
            schema: "public",
            table: "TodoItems",
            type: "uuid",
            nullable: false,
            defaultValue: Guid.Empty);

        migrationBuilder.AddColumn<int>(
            name: "Status",
            schema: "public",
            table: "TodoItems",
            type: "integer",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<int>(
            name: "TeamAssignedTo",
            schema: "public",
            table: "TodoItems",
            type: "integer",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddPrimaryKey(
            name: "PK_Users",
            schema: "public",
            table: "Users",
            column: "Id");

        migrationBuilder.AddPrimaryKey(
            name: "PK_TodoItems",
            schema: "public",
            table: "TodoItems",
            column: "Id");

        migrationBuilder.CreateIndex(
            name: "IX_TodoItems_AssignedTo",
            schema: "public",
            table: "TodoItems",
            column: "AssignedTo");

        migrationBuilder.AddForeignKey(
            name: "FK_TodoItems_Users_AssignedTo",
            schema: "public",
            table: "TodoItems",
            column: "AssignedTo",
            principalSchema: "public",
            principalTable: "Users",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_TodoItems_Users_RequestedBy",
            schema: "public",
            table: "TodoItems",
            column: "RequestedBy",
            principalSchema: "public",
            principalTable: "Users",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_TodoItems_Users_AssignedTo",
            schema: "public",
            table: "TodoItems");

        migrationBuilder.DropForeignKey(
            name: "FK_TodoItems_Users_RequestedBy",
            schema: "public",
            table: "TodoItems");

        migrationBuilder.DropPrimaryKey(
            name: "PK_Users",
            schema: "public",
            table: "Users");

        migrationBuilder.DropPrimaryKey(
            name: "PK_TodoItems",
            schema: "public",
            table: "TodoItems");

        migrationBuilder.DropIndex(
            name: "IX_TodoItems_AssignedTo",
            schema: "public",
            table: "TodoItems");

        migrationBuilder.DropColumn(
            name: "Team",
            schema: "public",
            table: "Users");

        migrationBuilder.DropColumn(
            name: "UserName",
            schema: "public",
            table: "Users");

        migrationBuilder.DropColumn(
            name: "AssignedTo",
            schema: "public",
            table: "TodoItems");

        migrationBuilder.DropColumn(
            name: "Status",
            schema: "public",
            table: "TodoItems");

        migrationBuilder.DropColumn(
            name: "TeamAssignedTo",
            schema: "public",
            table: "TodoItems");

        migrationBuilder.RenameTable(
            name: "Users",
            schema: "public",
            newName: "users",
            newSchema: "public");

        migrationBuilder.RenameTable(
            name: "TodoItems",
            schema: "public",
            newName: "todo_items",
            newSchema: "public");

        migrationBuilder.RenameColumn(
            name: "Email",
            schema: "public",
            table: "users",
            newName: "email");

        migrationBuilder.RenameColumn(
            name: "Id",
            schema: "public",
            table: "users",
            newName: "id");

        migrationBuilder.RenameColumn(
            name: "PasswordHash",
            schema: "public",
            table: "users",
            newName: "password_hash");

        migrationBuilder.RenameColumn(
            name: "LastName",
            schema: "public",
            table: "users",
            newName: "last_name");

        migrationBuilder.RenameColumn(
            name: "FirstName",
            schema: "public",
            table: "users",
            newName: "first_name");

        migrationBuilder.RenameIndex(
            name: "IX_Users_Email",
            schema: "public",
            table: "users",
            newName: "ix_users_email");

        migrationBuilder.RenameColumn(
            name: "Priority",
            schema: "public",
            table: "todo_items",
            newName: "priority");

        migrationBuilder.RenameColumn(
            name: "Labels",
            schema: "public",
            table: "todo_items",
            newName: "labels");

        migrationBuilder.RenameColumn(
            name: "Description",
            schema: "public",
            table: "todo_items",
            newName: "description");

        migrationBuilder.RenameColumn(
            name: "Id",
            schema: "public",
            table: "todo_items",
            newName: "id");

        migrationBuilder.RenameColumn(
            name: "IsCompleted",
            schema: "public",
            table: "todo_items",
            newName: "is_completed");

        migrationBuilder.RenameColumn(
            name: "DueDate",
            schema: "public",
            table: "todo_items",
            newName: "due_date");

        migrationBuilder.RenameColumn(
            name: "CreatedAt",
            schema: "public",
            table: "todo_items",
            newName: "created_at");

        migrationBuilder.RenameColumn(
            name: "CompletedAt",
            schema: "public",
            table: "todo_items",
            newName: "completed_at");

        migrationBuilder.RenameColumn(
            name: "RequestedBy",
            schema: "public",
            table: "todo_items",
            newName: "user_id");

        migrationBuilder.RenameIndex(
            name: "IX_TodoItems_RequestedBy",
            schema: "public",
            table: "todo_items",
            newName: "ix_todo_items_user_id");

        migrationBuilder.AddPrimaryKey(
            name: "pk_users",
            schema: "public",
            table: "users",
            column: "id");

        migrationBuilder.AddPrimaryKey(
            name: "pk_todo_items",
            schema: "public",
            table: "todo_items",
            column: "id");

        migrationBuilder.AddForeignKey(
            name: "fk_todo_items_users_user_id",
            schema: "public",
            table: "todo_items",
            column: "user_id",
            principalSchema: "public",
            principalTable: "users",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);
    }
}
