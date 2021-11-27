﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Playground.PaymentEngine.Store.EF.Accounts;

#nullable disable

namespace Playground.PaymentEngine.Store.EF.Migrations
{
    [DbContext(typeof(EFAccountStore))]
    partial class EFAccountStoreModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Playground.PaymentEngine.Store.Accounts.Model.Account", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("AccountTypeId")
                        .HasColumnType("bigint");

                    b.Property<long>("CustomerId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Exposure")
                        .HasColumnType("numeric");

                    b.Property<bool>("IsPreferredAccount")
                        .HasColumnType("boolean");

                    b.Property<long>("TenantId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AccountTypeId");

                    b.ToTable("Account", "accounts");

                    b.HasData(
                        new
                        {
                            Id = 267L,
                            AccountTypeId = 88L,
                            CustomerId = 2L,
                            Exposure = 30.0m,
                            IsPreferredAccount = false,
                            TenantId = 1L
                        },
                        new
                        {
                            Id = 300L,
                            AccountTypeId = 90L,
                            CustomerId = 44L,
                            Exposure = 132.0m,
                            IsPreferredAccount = true,
                            TenantId = 1L
                        },
                        new
                        {
                            Id = 567L,
                            AccountTypeId = 98L,
                            CustomerId = 44L,
                            Exposure = 3.0m,
                            IsPreferredAccount = false,
                            TenantId = 1L
                        },
                        new
                        {
                            Id = 747L,
                            AccountTypeId = 88L,
                            CustomerId = 74L,
                            Exposure = 0m,
                            IsPreferredAccount = false,
                            TenantId = 1L
                        });
                });

            modelBuilder.Entity("Playground.PaymentEngine.Store.Accounts.Model.AccountType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<decimal>("Charge")
                        .HasColumnType("numeric");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("ProcessOrder")
                        .HasColumnType("integer");

                    b.Property<long>("TenantId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Threshold")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("AccountType", "accounts");

                    b.HasData(
                        new
                        {
                            Id = 88L,
                            Charge = 0m,
                            Name = "Visa",
                            ProcessOrder = 0,
                            TenantId = 1L,
                            Threshold = 0m
                        },
                        new
                        {
                            Id = 90L,
                            Charge = 0m,
                            Name = "MasterCard",
                            ProcessOrder = 1,
                            TenantId = 1L,
                            Threshold = 0m
                        },
                        new
                        {
                            Id = 98L,
                            Charge = 0m,
                            Name = "PayPal",
                            ProcessOrder = 2,
                            TenantId = 1L,
                            Threshold = 0m
                        });
                });

            modelBuilder.Entity("Playground.PaymentEngine.Store.Model.MetaData", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long?>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("TenantId")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("MetaData", "accounts");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            AccountId = 267L,
                            Name = "account-holder",
                            TenantId = 1L,
                            Value = "E.L. Marè"
                        },
                        new
                        {
                            Id = 2L,
                            AccountId = 267L,
                            Name = "card-number",
                            TenantId = 1L,
                            Value = "123556456"
                        },
                        new
                        {
                            Id = 3L,
                            AccountId = 267L,
                            Name = "cvc",
                            TenantId = 1L,
                            Value = "547"
                        },
                        new
                        {
                            Id = 4L,
                            AccountId = 300L,
                            Name = "account-holder",
                            TenantId = 1L,
                            Value = "Kate Summers"
                        },
                        new
                        {
                            Id = 5L,
                            AccountId = 300L,
                            Name = "card-number",
                            TenantId = 1L,
                            Value = "556688112"
                        },
                        new
                        {
                            Id = 6L,
                            AccountId = 300L,
                            Name = "cvc",
                            TenantId = 1L,
                            Value = "556"
                        },
                        new
                        {
                            Id = 7L,
                            AccountId = 567L,
                            Name = "account",
                            TenantId = 1L,
                            Value = "ettiene@test.com"
                        },
                        new
                        {
                            Id = 8L,
                            AccountId = 567L,
                            Name = "sub-account",
                            TenantId = 1L,
                            Value = "savings"
                        },
                        new
                        {
                            Id = 9L,
                            AccountId = 747L,
                            Name = "account-holder",
                            TenantId = 1L,
                            Value = "Kate Moss"
                        },
                        new
                        {
                            Id = 10L,
                            AccountId = 747L,
                            Name = "card-number",
                            TenantId = 1L,
                            Value = "8675894885776"
                        },
                        new
                        {
                            Id = 11L,
                            AccountId = 747L,
                            Name = "cvc",
                            TenantId = 1L,
                            Value = "101"
                        });
                });

            modelBuilder.Entity("Playground.PaymentEngine.Store.Accounts.Model.Account", b =>
                {
                    b.HasOne("Playground.PaymentEngine.Store.Accounts.Model.AccountType", null)
                        .WithMany()
                        .HasForeignKey("AccountTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Playground.PaymentEngine.Store.Model.MetaData", b =>
                {
                    b.HasOne("Playground.PaymentEngine.Store.Accounts.Model.Account", null)
                        .WithMany("MetaData")
                        .HasForeignKey("AccountId");
                });

            modelBuilder.Entity("Playground.PaymentEngine.Store.Accounts.Model.Account", b =>
                {
                    b.Navigation("MetaData");
                });
#pragma warning restore 612, 618
        }
    }
}
