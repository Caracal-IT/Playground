﻿// <auto-generated />

using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Playground.PaymentEngine.Store.EF.Allocations;

#nullable disable

namespace Playground.PaymentEngine.Store.EF.Migrations.EFAllocationStoreMigrations
{
    [DbContext(typeof(EFAllocationStore))]
    [Migration("20211128152851_allocation_setup")]
    partial class AllocationSetup
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Playground.PaymentEngine.Store.Allocations.Model.Allocation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<long>("AllocationStatusId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Charge")
                        .HasColumnType("numeric");

                    b.Property<string>("Reference")
                        .HasColumnType("text");

                    b.Property<long>("TenantId")
                        .HasColumnType("bigint");

                    b.Property<string>("Terminal")
                        .HasColumnType("text");

                    b.Property<long>("WithdrawalGroupId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AllocationStatusId");

                    b.ToTable("Allocation", "allocations");

                    b.HasData(
                        new
                        {
                            Id = 189L,
                            AccountId = 267L,
                            AllocationStatusId = 1L,
                            Amount = 13m,
                            Charge = 0m,
                            TenantId = 1L,
                            WithdrawalGroupId = 1L
                        },
                        new
                        {
                            Id = 190L,
                            AccountId = 567L,
                            AllocationStatusId = 1L,
                            Amount = 27m,
                            Charge = 0m,
                            TenantId = 1L,
                            WithdrawalGroupId = 1L
                        },
                        new
                        {
                            Id = 200L,
                            AccountId = 300L,
                            AllocationStatusId = 1L,
                            Amount = 18m,
                            Charge = 0m,
                            TenantId = 1L,
                            WithdrawalGroupId = 1L
                        },
                        new
                        {
                            Id = 201L,
                            AccountId = 267L,
                            AllocationStatusId = 1L,
                            Amount = 12m,
                            Charge = 0m,
                            TenantId = 1L,
                            WithdrawalGroupId = 1L
                        },
                        new
                        {
                            Id = 202L,
                            AccountId = 300L,
                            AllocationStatusId = 1L,
                            Amount = 21m,
                            Charge = 0m,
                            TenantId = 1L,
                            WithdrawalGroupId = 1L
                        },
                        new
                        {
                            Id = 203L,
                            AccountId = 300L,
                            AllocationStatusId = 1L,
                            Amount = 29m,
                            Charge = 0m,
                            TenantId = 1L,
                            WithdrawalGroupId = 1L
                        },
                        new
                        {
                            Id = 671L,
                            AccountId = 747L,
                            AllocationStatusId = 1L,
                            Amount = 670m,
                            Charge = 0m,
                            Reference = "EF45_66_88",
                            TenantId = 1L,
                            Terminal = "Rebilly",
                            WithdrawalGroupId = 1L
                        });
                });

            modelBuilder.Entity("Playground.PaymentEngine.Store.Allocations.Model.AllocationStatus", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("TenantId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("AccountType", "allocations");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "allocated",
                            TenantId = 1L
                        },
                        new
                        {
                            Id = 2L,
                            Name = "refunded",
                            TenantId = 1L
                        },
                        new
                        {
                            Id = 3L,
                            Name = "confiscated",
                            TenantId = 1L
                        },
                        new
                        {
                            Id = 4L,
                            Name = "paid",
                            TenantId = 1L
                        },
                        new
                        {
                            Id = 5L,
                            Name = "rejected",
                            TenantId = 1L
                        },
                        new
                        {
                            Id = 6L,
                            Name = "callback-paid",
                            TenantId = 1L
                        });
                });

            modelBuilder.Entity("Playground.PaymentEngine.Store.Allocations.Model.Allocation", b =>
                {
                    b.HasOne("Playground.PaymentEngine.Store.Allocations.Model.AllocationStatus", null)
                        .WithMany()
                        .HasForeignKey("AllocationStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
