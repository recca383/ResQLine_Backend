using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class FixedUserReportRelationship : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Reports_Users_ReportedBy",
            schema: "public",
            table: "Reports");

        migrationBuilder.RenameColumn(
            name: "ReportedBy",
            schema: "public",
            table: "Reports",
            newName: "ReportedById");

        migrationBuilder.RenameIndex(
            name: "IX_Reports_ReportedBy",
            schema: "public",
            table: "Reports",
            newName: "IX_Reports_ReportedById");

        migrationBuilder.AddForeignKey(
            name: "FK_Reports_Users_ReportedById",
            schema: "public",
            table: "Reports",
            column: "ReportedById",
            principalSchema: "public",
            principalTable: "Users",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Reports_Users_ReportedById",
            schema: "public",
            table: "Reports");

        migrationBuilder.RenameColumn(
            name: "ReportedById",
            schema: "public",
            table: "Reports",
            newName: "ReportedBy");

        migrationBuilder.RenameIndex(
            name: "IX_Reports_ReportedById",
            schema: "public",
            table: "Reports",
            newName: "IX_Reports_ReportedBy");

        migrationBuilder.AddForeignKey(
            name: "FK_Reports_Users_ReportedBy",
            schema: "public",
            table: "Reports",
            column: "ReportedBy",
            principalSchema: "public",
            principalTable: "Users",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
