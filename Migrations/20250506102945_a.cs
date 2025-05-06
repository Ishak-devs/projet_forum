using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Migrations
{
    /// <inheritdoc />
    public partial class a : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Utilisateurs_Id_classe",
                table: "Utilisateurs");

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateurs_Id_classe",
                table: "Utilisateurs",
                column: "Id_classe",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Utilisateurs_Id_classe",
                table: "Utilisateurs");

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateurs_Id_classe",
                table: "Utilisateurs",
                column: "Id_classe");
        }
    }
}
