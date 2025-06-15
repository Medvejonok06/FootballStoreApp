using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FootballStoreApp.Migrations
{
    /// <inheritdoc />
    public partial class updated_items_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "order_items_order_id_fkey",
                table: "order_items");

            migrationBuilder.DropForeignKey(
                name: "order_items_product_id_fkey",
                table: "order_items");

            migrationBuilder.DropForeignKey(
                name: "orders_customer_id_fkey",
                table: "orders");

            migrationBuilder.DropPrimaryKey(
                name: "products_pkey",
                table: "products");

            migrationBuilder.DropPrimaryKey(
                name: "orders_pkey",
                table: "orders");

            migrationBuilder.DropPrimaryKey(
                name: "order_items_pkey",
                table: "order_items");

            migrationBuilder.DropPrimaryKey(
                name: "customers_pkey",
                table: "customers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "order_date",
                table: "orders",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddPrimaryKey(
                name: "PK_products",
                table: "products",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_orders",
                table: "orders",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_order_items",
                table: "order_items",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_customers",
                table: "customers",
                column: "id");

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    IsOnSale = table.Column<bool>(type: "boolean", nullable: false),
                    PurchasedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SoldDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PurchasePrice = table.Column<decimal>(type: "numeric", nullable: true),
                    CurrentOrFinalPrice = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_order_items_orders_order_id",
                table: "order_items",
                column: "order_id",
                principalTable: "orders",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_order_items_products_product_id",
                table: "order_items",
                column: "product_id",
                principalTable: "products",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_customers_customer_id",
                table: "orders",
                column: "customer_id",
                principalTable: "customers",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_items_orders_order_id",
                table: "order_items");

            migrationBuilder.DropForeignKey(
                name: "FK_order_items_products_product_id",
                table: "order_items");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_customers_customer_id",
                table: "orders");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_products",
                table: "products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_orders",
                table: "orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_order_items",
                table: "order_items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_customers",
                table: "customers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "order_date",
                table: "orders",
                type: "timestamp without time zone",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "products_pkey",
                table: "products",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "orders_pkey",
                table: "orders",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "order_items_pkey",
                table: "order_items",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "customers_pkey",
                table: "customers",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "order_items_order_id_fkey",
                table: "order_items",
                column: "order_id",
                principalTable: "orders",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "order_items_product_id_fkey",
                table: "order_items",
                column: "product_id",
                principalTable: "products",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "orders_customer_id_fkey",
                table: "orders",
                column: "customer_id",
                principalTable: "customers",
                principalColumn: "id");
        }
    }
}
