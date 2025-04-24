using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Migrations
{
    /// <inheritdoc />
    public partial class m12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id_utilisateur",
                table: "Etablissements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Etablissements_Id_utilisateur",
                table: "Etablissements",
                column: "Id_utilisateur");

            migrationBuilder.AddForeignKey(
                name: "FK_Etablissements_Utilisateurs_Id_utilisateur",
                table: "Etablissements",
                column: "Id_utilisateur",
                principalTable: "Utilisateurs",
                principalColumn: "Id_utilisateur",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Etablissements_Utilisateurs_Id_utilisateur",
                table: "Etablissements");

            migrationBuilder.DropIndex(
                name: "IX_Etablissements_Id_utilisateur",
                table: "Etablissements");

            migrationBuilder.DropColumn(
                name: "Id_utilisateur",
                table: "Etablissements");
        }
    }
}
