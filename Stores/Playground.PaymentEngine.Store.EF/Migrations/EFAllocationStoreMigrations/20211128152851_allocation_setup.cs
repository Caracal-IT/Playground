using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Playground.PaymentEngine.Store.EF.Migrations.EFAllocationStoreMigrations
{
    public partial class AllocationSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "allocations");

            migrationBuilder.CreateTable(
                name: "AccountType",
                schema: "allocations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Allocation",
                schema: "allocations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WithdrawalGroupId = table.Column<long>(type: "bigint", nullable: false),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Charge = table.Column<decimal>(type: "numeric", nullable: false),
                    AllocationStatusId = table.Column<long>(type: "bigint", nullable: false),
                    Terminal = table.Column<string>(type: "text", nullable: true),
                    Reference = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Allocation_AccountType_AllocationStatusId",
                        column: x => x.AllocationStatusId,
                        principalSchema: "allocations",
                        principalTable: "AccountType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "allocations",
                table: "AccountType",
                columns: new[] { "Id", "Name", "TenantId" },
                values: new object[,]
                {
                    { 1L, "allocated", 1L },
                    { 2L, "refunded", 1L },
                    { 3L, "confiscated", 1L },
                    { 4L, "paid", 1L },
                    { 5L, "rejected", 1L },
                    { 6L, "callback-paid", 1L }
                });

            migrationBuilder.InsertData(
                schema: "allocations",
                table: "Allocation",
                columns: new[] { "Id", "AccountId", "AllocationStatusId", "Amount", "Charge", "Reference", "TenantId", "Terminal", "WithdrawalGroupId" },
                values: new object[,]
                {
                    { 189L, 267L, 1L, 13m, 0m, null, 1L, null, 1L },
                    { 190L, 567L, 1L, 27m, 0m, null, 1L, null, 1L },
                    { 200L, 300L, 1L, 18m, 0m, null, 1L, null, 1L },
                    { 201L, 267L, 1L, 12m, 0m, null, 1L, null, 1L },
                    { 202L, 300L, 1L, 21m, 0m, null, 1L, null, 1L },
                    { 203L, 300L, 1L, 29m, 0m, null, 1L, null, 1L },
                    { 671L, 747L, 1L, 670m, 0m, "EF45_66_88", 1L, "Rebilly", 1L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Allocation_AllocationStatusId",
                schema: "allocations",
                table: "Allocation",
                column: "AllocationStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Allocation",
                schema: "allocations");

            migrationBuilder.DropTable(
                name: "AccountType",
                schema: "allocations");
        }
    }
}
