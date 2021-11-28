using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Playground.PaymentEngine.Store.EF.Migrations.EFApprovalRuleMigrations
{
    public partial class ApprovalRuleSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "approval_rules");

            migrationBuilder.CreateTable(
                name: "ApprovalRuleHistory",
                schema: "approval_rules",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WithdrawalGroupId = table.Column<long>(type: "bigint", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uuid", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalRuleHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApprovalRule",
                schema: "approval_rules",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RuleName = table.Column<string>(type: "text", nullable: false),
                    IsSuccessful = table.Column<bool>(type: "boolean", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: true),
                    ApprovalRuleHistoryId = table.Column<long>(type: "bigint", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalRule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovalRule_ApprovalRuleHistory_ApprovalRuleHistoryId",
                        column: x => x.ApprovalRuleHistoryId,
                        principalSchema: "approval_rules",
                        principalTable: "ApprovalRuleHistory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MetaData",
                schema: "approval_rules",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    ApprovalRuleHistoryId = table.Column<long>(type: "bigint", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetaData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MetaData_ApprovalRuleHistory_ApprovalRuleHistoryId",
                        column: x => x.ApprovalRuleHistoryId,
                        principalSchema: "approval_rules",
                        principalTable: "ApprovalRuleHistory",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                schema: "approval_rules",
                table: "ApprovalRuleHistory",
                columns: new[] { "Id", "TenantId", "TransactionDate", "TransactionId", "WithdrawalGroupId" },
                values: new object[] { 1L, 1L, new DateTime(2020, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("8c5c1ae6-b2ab-4715-aebe-258a64aee52d"), 2L });

            migrationBuilder.InsertData(
                schema: "approval_rules",
                table: "ApprovalRule",
                columns: new[] { "Id", "ApprovalRuleHistoryId", "IsSuccessful", "Message", "RuleName", "TenantId" },
                values: new object[,]
                {
                    { 1L, 1L, true, "Rule 1 succeeded", "rule1", 1L },
                    { 2L, 1L, true, "Rule 2 succeeded", "rule2", 1L }
                });

            migrationBuilder.InsertData(
                schema: "approval_rules",
                table: "MetaData",
                columns: new[] { "Id", "ApprovalRuleHistoryId", "Name", "TenantId", "Value" },
                values: new object[] { 1L, 1L, "withdrawal-id", 1L, "1" });

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRule_ApprovalRuleHistoryId",
                schema: "approval_rules",
                table: "ApprovalRule",
                column: "ApprovalRuleHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MetaData_ApprovalRuleHistoryId",
                schema: "approval_rules",
                table: "MetaData",
                column: "ApprovalRuleHistoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApprovalRule",
                schema: "approval_rules");

            migrationBuilder.DropTable(
                name: "MetaData",
                schema: "approval_rules");

            migrationBuilder.DropTable(
                name: "ApprovalRuleHistory",
                schema: "approval_rules");
        }
    }
}
