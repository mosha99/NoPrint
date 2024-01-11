using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoPrint.Ef.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "CustomerId_Seq",
                startValue: 1000L);

            migrationBuilder.CreateSequence(
                name: "InvoicesId_Seq",
                startValue: 1000L);

            migrationBuilder.CreateSequence(
                name: "ShopId_Seq",
                startValue: 1000L);

            migrationBuilder.CreateSequence(
                name: "UserId_Seq",
                startValue: 1000L);

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    _Id = table.Column<long>(type: "bigint", nullable: false),
                    User_Id = table.Column<long>(type: "bigint", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FillProfile = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x._Id);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    _Id = table.Column<long>(type: "bigint", nullable: false),
                    InvoiceId = table.Column<long>(type: "bigint", nullable: false),
                    Shop_Id = table.Column<long>(type: "bigint", nullable: false),
                    Customer_Id = table.Column<long>(type: "bigint", nullable: false),
                    RawCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinalCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x._Id);
                });

            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                {
                    _Id = table.Column<long>(type: "bigint", nullable: false),
                    User_Id = table.Column<long>(type: "bigint", nullable: false),
                    ShopName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShopAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shops", x => x._Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    _Id = table.Column<long>(type: "bigint", nullable: false),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TryLoginCount = table.Column<int>(type: "int", nullable: false),
                    CanLogin = table.Column<bool>(type: "bit", nullable: false),
                    ExpireDate_ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x._Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItem",
                columns: table => new
                {
                    Invoice_Id = table.Column<long>(type: "bigint", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Count = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RawFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinalFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItem", x => new { x.Invoice_Id, x.Id });
                    table.ForeignKey(
                        name: "FK_InvoiceItem_Invoices_Invoice_Id",
                        column: x => x.Invoice_Id,
                        principalTable: "Invoices",
                        principalColumn: "_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users_up",
                columns: table => new
                {
                    UserBase_Id = table.Column<long>(type: "bigint", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users_up", x => x.UserBase_Id);
                    table.ForeignKey(
                        name: "FK_Users_up_Users_UserBase_Id",
                        column: x => x.UserBase_Id,
                        principalTable: "Users",
                        principalColumn: "_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Visitors",
                columns: table => new
                {
                    UserBase_Id = table.Column<long>(type: "bigint", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: true),
                    CodeGenerateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitors", x => x.UserBase_Id);
                    table.ForeignKey(
                        name: "FK_Visitors_Users_UserBase_Id",
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
                name: "Customers");

            migrationBuilder.DropTable(
                name: "InvoiceItem");

            migrationBuilder.DropTable(
                name: "Shops");

            migrationBuilder.DropTable(
                name: "Users_up");

            migrationBuilder.DropTable(
                name: "Visitors");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropSequence(
                name: "CustomerId_Seq");

            migrationBuilder.DropSequence(
                name: "InvoicesId_Seq");

            migrationBuilder.DropSequence(
                name: "ShopId_Seq");

            migrationBuilder.DropSequence(
                name: "UserId_Seq");
        }
    }
}
