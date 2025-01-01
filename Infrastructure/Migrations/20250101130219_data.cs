using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCategories",
                table: "ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_ProductId",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "Hebele",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Products",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ProductCategories",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId1",
                table: "ProductCategories",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId1",
                table: "ProductCategories",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Details",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Details",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Brands",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCategories",
                table: "ProductCategories",
                columns: new[] { "ProductId", "CategoryId" });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "CreatedDate", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), false, "Elektronik" },
                    { 2, new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), false, "Mobilya" },
                    { 3, new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), true, "Kozmetik" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "IsDeleted", "Name", "ParentId", "Priority" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), false, "Elektrik", 0, 1 },
                    { 2, new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), false, "Moda", 0, 2 },
                    { 3, new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), false, "Bilgisayar", 1, 1 },
                    { 4, new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), false, "Kadın", 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "Details",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "Description", "IsDeleted", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Elektrik kategorisine ait detay açıklaması.", false, "Elektrik Detay" },
                    { 2, 3, new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Bilgisayar kategorisine ait detay açıklaması.", true, "Bilgisayar Detay" },
                    { 3, 4, new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Kadın kategorisine ait detay açıklaması.", false, "Kadın Detay" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "CreatedDate", "Description", "Discount", "IsDeleted", "Price", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Yüksek performanslı bir akıllı telefon.", 5.5m, false, 850.99m, "Telefon" },
                    { 2, 3, new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Yüksek çözünürlüklü ekran ve güçlü performans.", 7.2m, false, 1200.75m, "Laptop" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_CategoryId1",
                table: "ProductCategories",
                column: "CategoryId1");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_ProductId1",
                table: "ProductCategories",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_Categories_CategoryId1",
                table: "ProductCategories",
                column: "CategoryId1",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_Products_ProductId1",
                table: "ProductCategories",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_Categories_CategoryId1",
                table: "ProductCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_Products_ProductId1",
                table: "ProductCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCategories",
                table: "ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_CategoryId1",
                table: "ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_ProductId1",
                table: "ProductCategories");

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Details",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Details",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Details",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "ProductCategories");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Products",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ProductCategories",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Details",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Details",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Hebele",
                table: "Categories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Brands",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCategories",
                table: "ProductCategories",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_ProductId",
                table: "ProductCategories",
                column: "ProductId");
        }
    }
}
