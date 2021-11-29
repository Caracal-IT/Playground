using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Playground.PaymentEngine.Store.EF.Migrations.EFTerminalStoreMigrations
{
    public partial class TerminalSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "terminals");

            migrationBuilder.CreateTable(
                name: "Terminal",
                schema: "terminals",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: true),
                    RetryCount = table.Column<int>(type: "integer", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terminal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TerminalMap",
                schema: "terminals",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountTypeId = table.Column<long>(type: "bigint", nullable: false),
                    TerminalId = table.Column<long>(type: "bigint", nullable: false),
                    Order = table.Column<short>(type: "smallint", nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TerminalMap", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TerminalResult",
                schema: "terminals",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Success = table.Column<bool>(type: "boolean", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Reference = table.Column<string>(type: "text", nullable: true),
                    Terminal = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    Message = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TerminalResult", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Setting",
                schema: "terminals",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    TerminalId = table.Column<long>(type: "bigint", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Setting_Terminal_TerminalId",
                        column: x => x.TerminalId,
                        principalSchema: "terminals",
                        principalTable: "Terminal",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MetaData",
                schema: "terminals",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    TerminalResultId = table.Column<long>(type: "bigint", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetaData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MetaData_TerminalResult_TerminalResultId",
                        column: x => x.TerminalResultId,
                        principalSchema: "terminals",
                        principalTable: "TerminalResult",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                schema: "terminals",
                table: "Terminal",
                columns: new[] { "Id", "Name", "RetryCount", "TenantId", "Type" },
                values: new object[,]
                {
                    { 1L, "Rebilly", 2, 1L, "http" },
                    { 2L, "CustomTerminal", 2, 1L, "http" },
                    { 3L, "GlobalPay", 2, 1L, "http" },
                    { 4L, "Rebilly_File", 1, 1L, "stream" },
                    { 5L, "Orca", 1, 1L, "http" }
                });

            migrationBuilder.InsertData(
                schema: "terminals",
                table: "TerminalMap",
                columns: new[] { "Id", "AccountTypeId", "Enabled", "Order", "TenantId", "TerminalId" },
                values: new object[,]
                {
                    { 1L, 88L, true, (short)0, 1L, 1L },
                    { 2L, 98L, true, (short)0, 1L, 2L },
                    { 3L, 88L, true, (short)1, 1L, 3L },
                    { 4L, 90L, true, (short)1, 1L, 4L }
                });

            migrationBuilder.InsertData(
                schema: "terminals",
                table: "TerminalResult",
                columns: new[] { "Id", "Code", "Date", "Message", "Reference", "Success", "TenantId", "Terminal" },
                values: new object[] { 1L, "00", new DateTime(2000, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Test Message", "44543434", false, 1L, "Rebilly" });

            migrationBuilder.InsertData(
                schema: "terminals",
                table: "MetaData",
                columns: new[] { "Id", "Name", "TenantId", "TerminalResultId", "Value" },
                values: new object[,]
                {
                    { 1L, "account-holder", 1L, 1L, "E.L. Mare" },
                    { 2L, "card-number", 1L, 1L, "123556456" },
                    { 3L, "cvc", 1L, 1L, "547" }
                });

            migrationBuilder.InsertData(
                schema: "terminals",
                table: "Setting",
                columns: new[] { "Id", "Name", "TenantId", "TerminalId", "Value" },
                values: new object[,]
                {
                    { 1L, "header:auth-user", 1L, 1L, "Ettiene Mare" },
                    { 2L, "file_path", 1L, 4L, "Exports" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MetaData_TerminalResultId",
                schema: "terminals",
                table: "MetaData",
                column: "TerminalResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Setting_TerminalId",
                schema: "terminals",
                table: "Setting",
                column: "TerminalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MetaData",
                schema: "terminals");

            migrationBuilder.DropTable(
                name: "Setting",
                schema: "terminals");

            migrationBuilder.DropTable(
                name: "TerminalMap",
                schema: "terminals");

            migrationBuilder.DropTable(
                name: "TerminalResult",
                schema: "terminals");

            migrationBuilder.DropTable(
                name: "Terminal",
                schema: "terminals");
        }
    }
}
