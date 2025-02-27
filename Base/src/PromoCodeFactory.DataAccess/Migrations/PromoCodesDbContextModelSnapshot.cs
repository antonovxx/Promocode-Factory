﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PromoCodeFactory.DataAccess;

#nullable disable

namespace PromoCodeFactory.DataAccess.Migrations
{
    [DbContext(typeof(PromoCodesDbContext))]
    partial class PromoCodesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("PromoCodeFactory.Core.Administration.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<int>("AppliedPromoCodesCount")
                        .HasColumnType("INTEGER")
                        .HasColumnName("applied_promo_codes_count");

                    b.Property<DateTime>("CreatedAtUtc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("last_name");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("TEXT")
                        .HasColumnName("role_id");

                    b.Property<DateTime>("UpdatedAtUtc")
                        .HasColumnType("TEXT")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_employees");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_employees_role_id");

                    b.ToTable("employees", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("451533d5-d8d5-4a11-9c7b-eb9f14e1a32f"),
                            AppliedPromoCodesCount = 5,
                            CreatedAtUtc = new DateTime(2024, 11, 12, 6, 53, 37, 389, DateTimeKind.Utc).AddTicks(860),
                            Email = "owner@somemail.ru",
                            FirstName = "Иван",
                            LastName = "Сергеев",
                            RoleId = new Guid("53729686-a368-4eeb-8bfa-cc69b6050d02"),
                            UpdatedAtUtc = new DateTime(2024, 11, 12, 6, 53, 37, 389, DateTimeKind.Utc).AddTicks(863)
                        },
                        new
                        {
                            Id = new Guid("f766e2bf-340a-46ea-bff3-f1700b435895"),
                            AppliedPromoCodesCount = 10,
                            CreatedAtUtc = new DateTime(2024, 11, 12, 6, 53, 37, 389, DateTimeKind.Utc).AddTicks(866),
                            Email = "andreev@somemail.ru",
                            FirstName = "Петр",
                            LastName = "Андреев",
                            RoleId = new Guid("b0ae7aac-5493-45cd-ad16-87426a5e7665"),
                            UpdatedAtUtc = new DateTime(2024, 11, 12, 6, 53, 37, 389, DateTimeKind.Utc).AddTicks(867)
                        });
                });

            modelBuilder.Entity("PromoCodeFactory.Core.Administration.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAtUtc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.Property<DateTime>("UpdatedAtUtc")
                        .HasColumnType("TEXT")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_roles");

                    b.ToTable("roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("53729686-a368-4eeb-8bfa-cc69b6050d02"),
                            CreatedAtUtc = new DateTime(2024, 11, 12, 6, 53, 37, 389, DateTimeKind.Utc).AddTicks(5821),
                            Description = "Администратор",
                            Name = "Admin",
                            UpdatedAtUtc = new DateTime(2024, 11, 12, 6, 53, 37, 389, DateTimeKind.Utc).AddTicks(5824)
                        },
                        new
                        {
                            Id = new Guid("b0ae7aac-5493-45cd-ad16-87426a5e7665"),
                            CreatedAtUtc = new DateTime(2024, 11, 12, 6, 53, 37, 389, DateTimeKind.Utc).AddTicks(5827),
                            Description = "Партнерский менеджер",
                            Name = "PartnerManager",
                            UpdatedAtUtc = new DateTime(2024, 11, 12, 6, 53, 37, 389, DateTimeKind.Utc).AddTicks(5827)
                        });
                });

            modelBuilder.Entity("PromoCodeFactory.Core.PromoCodeManagement.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAtUtc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("last_name");

                    b.Property<DateTime>("UpdatedAtUtc")
                        .HasColumnType("TEXT")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_customers");

                    b.ToTable("customers", (string)null);
                });

            modelBuilder.Entity("PromoCodeFactory.Core.PromoCodeManagement.CustomerPreference", b =>
                {
                    b.Property<Guid>("CustomerId")
                        .HasColumnType("TEXT")
                        .HasColumnName("customer_id");

                    b.Property<Guid>("PreferenceId")
                        .HasColumnType("TEXT")
                        .HasColumnName("preference_id");

                    b.HasKey("CustomerId", "PreferenceId")
                        .HasName("pk_customer_preferences");

                    b.HasIndex("PreferenceId")
                        .HasDatabaseName("ix_customer_preferences_preference_id");

                    b.ToTable("customer_preferences", (string)null);
                });

            modelBuilder.Entity("PromoCodeFactory.Core.PromoCodeManagement.Preference", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAtUtc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.Property<DateTime>("UpdatedAtUtc")
                        .HasColumnType("TEXT")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_preferences");

                    b.ToTable("preferences", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("848deaf5-b346-4f7a-b903-cdb97d274e99"),
                            CreatedAtUtc = new DateTime(2024, 11, 12, 6, 53, 37, 389, DateTimeKind.Utc).AddTicks(1684),
                            Name = "Скидки",
                            UpdatedAtUtc = new DateTime(2024, 11, 12, 6, 53, 37, 389, DateTimeKind.Utc).AddTicks(1684)
                        },
                        new
                        {
                            Id = new Guid("001a42d4-401e-4222-b0a3-f0b7c020bc2b"),
                            CreatedAtUtc = new DateTime(2024, 11, 12, 6, 53, 37, 389, DateTimeKind.Utc).AddTicks(1687),
                            Name = "Акции",
                            UpdatedAtUtc = new DateTime(2024, 11, 12, 6, 53, 37, 389, DateTimeKind.Utc).AddTicks(1687)
                        },
                        new
                        {
                            Id = new Guid("9dd84697-36d2-43f8-bdcc-1ebcd61da316"),
                            CreatedAtUtc = new DateTime(2024, 11, 12, 6, 53, 37, 389, DateTimeKind.Utc).AddTicks(1689),
                            Name = "Распродажи",
                            UpdatedAtUtc = new DateTime(2024, 11, 12, 6, 53, 37, 389, DateTimeKind.Utc).AddTicks(1689)
                        },
                        new
                        {
                            Id = new Guid("db29f052-9b5e-46cc-8db7-0c24f51be0a9"),
                            CreatedAtUtc = new DateTime(2024, 11, 12, 6, 53, 37, 389, DateTimeKind.Utc).AddTicks(1690),
                            Name = "Новинки",
                            UpdatedAtUtc = new DateTime(2024, 11, 12, 6, 53, 37, 389, DateTimeKind.Utc).AddTicks(1691)
                        });
                });

            modelBuilder.Entity("PromoCodeFactory.Core.PromoCodeManagement.PromoCode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<DateTime>("BeginDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("begin_date");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("code");

                    b.Property<DateTime>("CreatedAtUtc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("TEXT")
                        .HasColumnName("customer_id");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("end_date");

                    b.Property<Guid>("PartnerManagerId")
                        .HasColumnType("TEXT")
                        .HasColumnName("partner_manager_id");

                    b.Property<string>("PartnerName")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("partner_name");

                    b.Property<Guid>("PreferenceId")
                        .HasColumnType("TEXT")
                        .HasColumnName("preference_id");

                    b.Property<string>("ServiceInfo")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT")
                        .HasColumnName("service_info");

                    b.Property<DateTime>("UpdatedAtUtc")
                        .HasColumnType("TEXT")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_promo_codes");

                    b.HasIndex("CustomerId")
                        .HasDatabaseName("ix_promo_codes_customer_id");

                    b.HasIndex("PartnerManagerId")
                        .HasDatabaseName("ix_promo_codes_partner_manager_id");

                    b.HasIndex("PreferenceId")
                        .HasDatabaseName("ix_promo_codes_preference_id");

                    b.ToTable("promo_codes", (string)null);
                });

            modelBuilder.Entity("PromoCodeFactory.Core.Administration.Employee", b =>
                {
                    b.HasOne("PromoCodeFactory.Core.Administration.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_employees_roles_role_id");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("PromoCodeFactory.Core.PromoCodeManagement.CustomerPreference", b =>
                {
                    b.HasOne("PromoCodeFactory.Core.PromoCodeManagement.Customer", "Customer")
                        .WithMany("CustomerPreferences")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_customer_preferences_customers_customer_id");

                    b.HasOne("PromoCodeFactory.Core.PromoCodeManagement.Preference", "Preference")
                        .WithMany("CustomerPreferences")
                        .HasForeignKey("PreferenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_customer_preferences_preferences_preference_id");

                    b.Navigation("Customer");

                    b.Navigation("Preference");
                });

            modelBuilder.Entity("PromoCodeFactory.Core.PromoCodeManagement.PromoCode", b =>
                {
                    b.HasOne("PromoCodeFactory.Core.PromoCodeManagement.Customer", "Customer")
                        .WithMany("PromoCodes")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("fk_promo_codes_customers_customer_id");

                    b.HasOne("PromoCodeFactory.Core.Administration.Employee", "PartnerManager")
                        .WithMany()
                        .HasForeignKey("PartnerManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_promo_codes_employees_partner_manager_id");

                    b.HasOne("PromoCodeFactory.Core.PromoCodeManagement.Preference", "Preference")
                        .WithMany()
                        .HasForeignKey("PreferenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_promo_codes_preferences_preference_id");

                    b.Navigation("Customer");

                    b.Navigation("PartnerManager");

                    b.Navigation("Preference");
                });

            modelBuilder.Entity("PromoCodeFactory.Core.PromoCodeManagement.Customer", b =>
                {
                    b.Navigation("CustomerPreferences");

                    b.Navigation("PromoCodes");
                });

            modelBuilder.Entity("PromoCodeFactory.Core.PromoCodeManagement.Preference", b =>
                {
                    b.Navigation("CustomerPreferences");
                });
#pragma warning restore 612, 618
        }
    }
}
