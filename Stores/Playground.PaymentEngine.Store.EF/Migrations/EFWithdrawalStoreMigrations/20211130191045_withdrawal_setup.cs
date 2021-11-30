using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Playground.PaymentEngine.Store.EF.Migrations.EFWithdrawalStoreMigrations
{
    public partial class WithdrawalSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "withdrawals");

            migrationBuilder.CreateTable(
                name: "Withdrawal",
                schema: "withdrawals",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    DateRequested = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    WithdrawalStatusId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Withdrawal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WithdrawalGroup",
                schema: "withdrawals",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    WithdrawalIds = table.Column<string>(type: "text", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WithdrawalGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WithdrawalStatus",
                schema: "withdrawals",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WithdrawalStatus", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "withdrawals",
                table: "Withdrawal",
                columns: new[] { "Id", "Amount", "CustomerId", "DateRequested", "IsDeleted", "TenantId", "WithdrawalStatusId" },
                values: new object[,]
                {
                    { 1L, 50m, 44L, new DateTime(2021, 8, 12, 0, 0, 0, 0, DateTimeKind.Utc), false, 1L, 3L },
                    { 2L, 50m, 44L, new DateTime(2021, 8, 13, 0, 0, 0, 0, DateTimeKind.Utc), false, 1L, 3L },
                    { 3L, 460m, 74L, new DateTime(2021, 8, 17, 0, 0, 0, 0, DateTimeKind.Utc), false, 1L, 3L }
                });

            migrationBuilder.InsertData(
                schema: "withdrawals",
                table: "WithdrawalGroup",
                columns: new[] { "Id", "CustomerId", "TenantId", "WithdrawalIds" },
                values: new object[,]
                {
                    { 1L, 44L, 1L, "1,2" },
                    { 2L, 74L, 1L, "3" }
                });

            migrationBuilder.InsertData(
                schema: "withdrawals",
                table: "WithdrawalStatus",
                columns: new[] { "Id", "Name", "TenantId" },
                values: new object[,]
                {
                    { 1L, "requested", 1L },
                    { 2L, "flashed", 1L },
                    { 3L, "batched", 1L },
                    { 4L, "approved", 1L },
                    { 5L, "rejected", 1L },
                    { 6L, "failed", 1L },
                    { 7L, "partially-processed", 1L },
                    { 8L, "processed", 1L }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Withdrawal",
                schema: "withdrawals");

            migrationBuilder.DropTable(
                name: "WithdrawalGroup",
                schema: "withdrawals");

            migrationBuilder.DropTable(
                name: "WithdrawalStatus",
                schema: "withdrawals");
        }
    }
}
