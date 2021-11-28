using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Playground.PaymentEngine.Store.EF.Migrations.EFCustomerStoreMigrations
{
    public partial class CustomerSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "customers");

            migrationBuilder.CreateTable(
                name: "Customer",
                schema: "customers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Balance = table.Column<decimal>(type: "numeric", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MetaData",
                schema: "customers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetaData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MetaData_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "customers",
                        principalTable: "Customer",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                schema: "customers",
                table: "Customer",
                columns: new[] { "Id", "Balance", "TenantId" },
                values: new object[,]
                {
                    { 44L, 3400.0m, 1L },
                    { 74L, 1.0m, 1L }
                });

            migrationBuilder.InsertData(
                schema: "customers",
                table: "MetaData",
                columns: new[] { "Id", "CustomerId", "Name", "TenantId", "Value" },
                values: new object[,]
                {
                    { 1L, 44L, "firstName", 1L, "Ettiene" },
                    { 2L, 44L, "lastName", 1L, "Mare" },
                    { 3L, 44L, "hasKYC", 1L, "true" },
                    { 4L, 74L, "firstName", 1L, "Kate" },
                    { 5L, 74L, "lastName", 1L, "Moss" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MetaData_CustomerId",
                schema: "customers",
                table: "MetaData",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MetaData",
                schema: "customers");

            migrationBuilder.DropTable(
                name: "Customer",
                schema: "customers");
        }
    }
}
