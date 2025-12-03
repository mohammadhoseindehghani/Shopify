using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopify.Infa.Db.SqlServer.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUnitPriceFromCartItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "CartItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "CartItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
