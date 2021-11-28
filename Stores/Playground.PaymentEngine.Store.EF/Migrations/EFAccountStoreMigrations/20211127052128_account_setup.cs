using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Playground.PaymentEngine.Store.EF.Migrations.EFAccountStoreMigrations
{
    public partial class AccountSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "accounts");

            migrationBuilder.CreateTable(
                name: "AccountType",
                schema: "accounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Charge = table.Column<decimal>(type: "numeric", nullable: false),
                    Threshold = table.Column<decimal>(type: "numeric", nullable: false),
                    ProcessOrder = table.Column<int>(type: "integer", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                schema: "accounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountTypeId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    Exposure = table.Column<decimal>(type: "numeric", nullable: false),
                    IsPreferredAccount = table.Column<bool>(type: "boolean", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Account_AccountType_AccountTypeId",
                        column: x => x.AccountTypeId,
                        principalSchema: "accounts",
                        principalTable: "AccountType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MetaData",
                schema: "accounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    AccountId = table.Column<long>(type: "bigint", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetaData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MetaData_Account_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "accounts",
                        principalTable: "Account",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                schema: "accounts",
                table: "AccountType",
                columns: new[] { "Id", "Charge", "Name", "ProcessOrder", "TenantId", "Threshold" },
                values: new object[,]
                {
                    { 88L, 0m, "Visa", 0, 1L, 500m },
                    { 90L, 0m, "MasterCard", 1, 1L, 500m },
                    { 98L, 0m, "PayPal", 2, 1L, 0m }
                });

            migrationBuilder.InsertData(
                schema: "accounts",
                table: "Account",
                columns: new[] { "Id", "AccountTypeId", "CustomerId", "Exposure", "IsPreferredAccount", "TenantId" },
                values: new object[,]
                {
                    { 267L, 88L, 2L, 30.0m, false, 1L },
                    { 300L, 90L, 44L, 132.0m, true, 1L },
                    { 567L, 98L, 44L, 3.0m, false, 1L },
                    { 747L, 88L, 74L, 0m, false, 1L }
                });

            migrationBuilder.InsertData(
                schema: "accounts",
                table: "MetaData",
                columns: new[] { "Id", "AccountId", "Name", "TenantId", "Value" },
                values: new object[,]
                {
                    { 1L, 267L, "account-holder", 1L, "E.L. Marè" },
                    { 2L, 267L, "card-number", 1L, "123556456" },
                    { 3L, 267L, "cvc", 1L, "547" },
                    { 4L, 300L, "account-holder", 1L, "Kate Summers" },
                    { 5L, 300L, "card-number", 1L, "556688112" },
                    { 6L, 300L, "cvc", 1L, "556" },
                    { 7L, 567L, "account", 1L, "ettiene@test.com" },
                    { 8L, 567L, "sub-account", 1L, "savings" },
                    { 9L, 747L, "account-holder", 1L, "Kate Moss" },
                    { 10L, 747L, "card-number", 1L, "8675894885776" },
                    { 11L, 747L, "cvc", 1L, "101" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_AccountTypeId",
                schema: "accounts",
                table: "Account",
                column: "AccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MetaData_AccountId",
                schema: "accounts",
                table: "MetaData",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MetaData",
                schema: "accounts");

            migrationBuilder.DropTable(
                name: "Account",
                schema: "accounts");

            migrationBuilder.DropTable(
                name: "AccountType",
                schema: "accounts");
        }
    }
}
