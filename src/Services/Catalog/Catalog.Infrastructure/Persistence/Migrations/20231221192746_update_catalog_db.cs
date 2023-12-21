using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updatecatalogdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Weights_WeightCategories_WeightCategoryId",
                table: "Weights");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WeightCategories",
                table: "WeightCategories");

            migrationBuilder.DropColumn(
                name: "TaxCateCode",
                table: "products");

            migrationBuilder.RenameTable(
                name: "WeightCategories",
                newName: "weights");

            migrationBuilder.AddColumn<string>(
                name: "Alias",
                table: "suppliers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Alias",
                table: "product_variants",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "product_variants",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Alias",
                table: "categories",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "categories",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_weights",
                table: "weights",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Weights_weights_WeightCategoryId",
                table: "Weights",
                column: "WeightCategoryId",
                principalTable: "weights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Weights_weights_WeightCategoryId",
                table: "Weights");

            migrationBuilder.DropPrimaryKey(
                name: "PK_weights",
                table: "weights");

            migrationBuilder.DropColumn(
                name: "Alias",
                table: "suppliers");

            migrationBuilder.DropColumn(
                name: "Alias",
                table: "product_variants");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "product_variants");

            migrationBuilder.DropColumn(
                name: "Alias",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "categories");

            migrationBuilder.RenameTable(
                name: "weights",
                newName: "WeightCategories");

            migrationBuilder.AddColumn<string>(
                name: "TaxCateCode",
                table: "products",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeightCategories",
                table: "WeightCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Weights_WeightCategories_WeightCategoryId",
                table: "Weights",
                column: "WeightCategoryId",
                principalTable: "WeightCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
