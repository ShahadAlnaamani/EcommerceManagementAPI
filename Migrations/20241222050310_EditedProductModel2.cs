using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class EditedProductModel2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CatId",
                table: "Products",
                newName: "Category");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Products",
                newName: "CatId");
        }
    }
}
