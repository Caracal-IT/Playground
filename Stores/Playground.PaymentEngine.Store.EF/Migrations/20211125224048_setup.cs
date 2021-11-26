using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Playground.PaymentEngine.Store.EF.Migrations
{
    public partial class setup : Migration
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
                values: new object[] { 1L, 0m, "Visa", 0, 1L, 0m });

            migrationBuilder.InsertData(
                schema: "accounts",
                table: "Account",
                columns: new[] { "Id", "AccountTypeId", "CustomerId", "Exposure", "IsPreferredAccount", "TenantId" },
                values: new object[] { 1L, 1L, 2L, 30.0m, true, 1L });

            migrationBuilder.InsertData(
                schema: "accounts",
                table: "MetaData",
                columns: new[] { "Id", "AccountId", "Name", "TenantId", "Value" },
                values: new object[] { 1L, 1L, "AccountHolder", 1L, "Kate" });

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
