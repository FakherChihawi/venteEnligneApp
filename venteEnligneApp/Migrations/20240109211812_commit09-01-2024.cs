using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace venteEnligneApp.Migrations
{
    /// <inheritdoc />
    public partial class commit09012024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantite",
                table: "Articles",
                newName: "Quantite");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantite",
                table: "Articles",
                newName: "Quantite");
        }
    }
}
