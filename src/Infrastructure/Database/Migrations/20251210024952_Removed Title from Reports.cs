using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations;
    /// <inheritdoc />
public partial class RemovedTitlefromReports : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Title",
            schema: "public",
            table: "Reports");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "Title",
            schema: "public",
            table: "Reports",
            type: "text",
            nullable: false,
            defaultValue: "");
    }
}

