using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Compar.MD.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyProductTwo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsTop",
                table: "Products",
                newName: "IsSelected");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsSelected",
                table: "Products",
                newName: "IsTop");
        }
    }
}
