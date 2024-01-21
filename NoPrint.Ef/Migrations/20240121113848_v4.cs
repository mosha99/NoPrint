using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoPrint.Ef.Migrations
{
    /// <inheritdoc />
    public partial class v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpireDate_ExpireDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastLogin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LoginId",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "LoginInstance",
                columns: table => new
                {
                    UserBase_Id = table.Column<long>(type: "bigint", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpireDate_ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LoginId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeviceInfo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginInstance", x => new { x.UserBase_Id, x.Id });
                    table.ForeignKey(
                        name: "FK_LoginInstance_Users_UserBase_Id",
                        column: x => x.UserBase_Id,
                        principalTable: "Users",
                        principalColumn: "_Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoginInstance");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireDate_ExpireDate",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLogin",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LoginId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
