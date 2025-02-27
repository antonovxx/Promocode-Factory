﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PromoCodeFactory.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    first_name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(2024, 10, 22, 18, 19, 4, 97, DateTimeKind.Utc).AddTicks(5890)),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(2024, 10, 22, 18, 19, 4, 97, DateTimeKind.Utc).AddTicks(6340))
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "preferences",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(2024, 10, 22, 18, 19, 4, 98, DateTimeKind.Utc).AddTicks(2450)),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(2024, 10, 22, 18, 19, 4, 98, DateTimeKind.Utc).AddTicks(2910))
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_preferences", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(2024, 10, 22, 18, 19, 4, 98, DateTimeKind.Utc).AddTicks(8470)),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(2024, 10, 22, 18, 19, 4, 98, DateTimeKind.Utc).AddTicks(8910))
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "customer_preferences",
                columns: table => new
                {
                    customer_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    preference_id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customer_preferences", x => new { x.customer_id, x.preference_id });
                    table.ForeignKey(
                        name: "fk_customer_preferences_customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_customer_preferences_preferences_preference_id",
                        column: x => x.preference_id,
                        principalTable: "preferences",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    first_name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    role_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    applied_promo_codes_count = table.Column<int>(type: "INTEGER", nullable: false),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(2024, 10, 22, 18, 19, 4, 97, DateTimeKind.Utc).AddTicks(9830)),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(2024, 10, 22, 18, 19, 4, 98, DateTimeKind.Utc).AddTicks(330))
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_employees", x => x.id);
                    table.ForeignKey(
                        name: "fk_employees_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "promo_codes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    code = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    service_info = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    begin_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    end_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    partner_name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    partner_manager_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    preference_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    customer_id = table.Column<Guid>(type: "TEXT", nullable: true),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(2024, 10, 22, 18, 19, 4, 98, DateTimeKind.Utc).AddTicks(3810)),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(2024, 10, 22, 18, 19, 4, 98, DateTimeKind.Utc).AddTicks(4240))
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_promo_codes", x => x.id);
                    table.ForeignKey(
                        name: "fk_promo_codes_customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_promo_codes_employees_partner_manager_id",
                        column: x => x.partner_manager_id,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_promo_codes_preferences_preference_id",
                        column: x => x.preference_id,
                        principalTable: "preferences",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "preferences",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("001a42d4-401e-4222-b0a3-f0b7c020bc2b"), "Акции" },
                    { new Guid("848deaf5-b346-4f7a-b903-cdb97d274e99"), "Скидки" },
                    { new Guid("9dd84697-36d2-43f8-bdcc-1ebcd61da316"), "Распродажи" },
                    { new Guid("db29f052-9b5e-46cc-8db7-0c24f51be0a9"), "Новинки" }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "description", "name" },
                values: new object[,]
                {
                    { new Guid("53729686-a368-4eeb-8bfa-cc69b6050d02"), "Администратор", "Admin" },
                    { new Guid("b0ae7aac-5493-45cd-ad16-87426a5e7665"), "Партнерский менеджер", "PartnerManager" }
                });

            migrationBuilder.InsertData(
                table: "employees",
                columns: new[] { "id", "applied_promo_codes_count", "email", "first_name", "last_name", "role_id" },
                values: new object[,]
                {
                    { new Guid("451533d5-d8d5-4a11-9c7b-eb9f14e1a32f"), 5, "owner@somemail.ru", "Иван", "Сергеев", new Guid("53729686-a368-4eeb-8bfa-cc69b6050d02") },
                    { new Guid("f766e2bf-340a-46ea-bff3-f1700b435895"), 10, "andreev@somemail.ru", "Петр", "Андреев", new Guid("b0ae7aac-5493-45cd-ad16-87426a5e7665") }
                });

            migrationBuilder.CreateIndex(
                name: "ix_customer_preferences_preference_id",
                table: "customer_preferences",
                column: "preference_id");

            migrationBuilder.CreateIndex(
                name: "ix_employees_role_id",
                table: "employees",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_promo_codes_customer_id",
                table: "promo_codes",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "ix_promo_codes_partner_manager_id",
                table: "promo_codes",
                column: "partner_manager_id");

            migrationBuilder.CreateIndex(
                name: "ix_promo_codes_preference_id",
                table: "promo_codes",
                column: "preference_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customer_preferences");

            migrationBuilder.DropTable(
                name: "promo_codes");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "preferences");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
