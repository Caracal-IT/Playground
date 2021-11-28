using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Playground.PaymentEngine.Store.EF.Migrations.EFDepositStoreMigrations
{
    public partial class DepositSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "deposits");

            migrationBuilder.CreateTable(
                name: "Deposit",
                schema: "deposits",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    DepositDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MetaData",
                schema: "deposits",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    DepositId = table.Column<long>(type: "bigint", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetaData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MetaData_Deposit_DepositId",
                        column: x => x.DepositId,
                        principalSchema: "deposits",
                        principalTable: "Deposit",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                schema: "deposits",
                table: "Deposit",
                columns: new[] { "Id", "AccountId", "Amount", "DepositDate", "TenantId" },
                values: new object[,]
                {
                    { 2L, 267L, 220.0m, new DateTime(2021, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), 1L },
                    { 3L, 267L, 110.0m, new DateTime(2021, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), 1L },
                    { 4L, 300L, 200.0m, new DateTime(2021, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), 1L },
                    { 5L, 567L, 30.0m, new DateTime(2021, 6, 17, 0, 0, 0, 0, DateTimeKind.Utc), 1L },
                    { 6L, 747L, 670.0m, new DateTime(2021, 7, 15, 0, 0, 0, 0, DateTimeKind.Utc), 1L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MetaData_DepositId",
                schema: "deposits",
                table: "MetaData",
                column: "DepositId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MetaData",
                schema: "deposits");

            migrationBuilder.DropTable(
                name: "Deposit",
                schema: "deposits");
        }
    }
}
