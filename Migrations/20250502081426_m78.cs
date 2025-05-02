using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Migrations
{
    /// <inheritdoc />
    public partial class m78 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "numero_rue",
                table: "Etablissements");

            migrationBuilder.RenameColumn(
                name: "ville",
                table: "Etablissements",
                newName: "commune");

            migrationBuilder.RenameColumn(
                name: "nom_rue",
                table: "Etablissements",
                newName: "adresse");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "commune",
                table: "Etablissements",
                newName: "ville");

            migrationBuilder.RenameColumn(
                name: "adresse",
                table: "Etablissements",
                newName: "nom_rue");

            migrationBuilder.AddColumn<int>(
                name: "numero_rue",
                table: "Etablissements",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
