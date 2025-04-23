using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Migrations
{
    /// <inheritdoc />
    public partial class m3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Messages_Id_utilisateur",
                table: "Messages",
                column: "Id_utilisateur");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Utilisateurs_Id_utilisateur",
                table: "Messages",
                column: "Id_utilisateur",
                principalTable: "Utilisateurs",
                principalColumn: "Id_utilisateur",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Utilisateurs_Id_utilisateur",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_Id_utilisateur",
                table: "Messages");
        }
    }
}
