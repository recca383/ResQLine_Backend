using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Store_Multiple_Images_Per_Report : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<List<byte[]>>(
            name: "Image",
            schema: "public",
            table: "Reports",
            type: "bytea[]",
            nullable: false,
            oldClrType: typeof(byte[]),
            oldType: "bytea");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<byte[]>(
            name: "Image",
            schema: "public",
            table: "Reports",
            type: "bytea",
            nullable: false,
            oldClrType: typeof(List<byte[]>),
            oldType: "bytea[]");
    }
}
