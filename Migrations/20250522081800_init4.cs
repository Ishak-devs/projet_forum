using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Migrations
{
    /// <inheritdoc />
    public partial class init4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id_utilisateur",
                table: "Notes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Note",
                table: "Notes",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_Id_utilisateur",
                table: "Notes",
                column: "Id_utilisateur");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Utilisateurs_Id_utilisateur",
                table: "Notes",
                column: "Id_utilisateur",
                principalTable: "Utilisateurs",
                principalColumn: "Id_utilisateur",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Utilisateurs_Id_utilisateur",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_Id_utilisateur",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "Id_utilisateur",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Notes");
        }
    }
}
