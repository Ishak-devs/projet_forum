using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Migrations
{
    /// <inheritdoc />
    public partial class m2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Utilisateurs_Id_utilisateur",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_Id_utilisateur",
                table: "Messages");

            migrationBuilder.AddColumn<int>(
                name: "Id_promo",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_Id_promo",
                table: "Messages",
                column: "Id_promo");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Promos_Id_promo",
                table: "Messages",
                column: "Id_promo",
                principalTable: "Promos",
                principalColumn: "Id_promo",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Promos_Id_promo",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_Id_promo",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Id_promo",
                table: "Messages");

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
    }
}
