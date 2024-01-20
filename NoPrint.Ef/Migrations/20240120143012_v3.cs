using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoPrint.Ef.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "Invoices");

            migrationBuilder.AddColumn<DateTime>(
                name: "EnterDateTime",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnterDateTime",
                table: "Invoices");

            migrationBuilder.AddColumn<long>(
                name: "InvoiceId",
                table: "Invoices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
