using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Utilisateurs_Promos_Id_promo",
                table: "Utilisateurs");

            migrationBuilder.AlterColumn<int>(
                name: "Id_promo",
                table: "Utilisateurs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Utilisateurs_Promos_Id_promo",
                table: "Utilisateurs",
                column: "Id_promo",
                principalTable: "Promos",
                principalColumn: "Id_promo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Utilisateurs_Promos_Id_promo",
                table: "Utilisateurs");

            migrationBuilder.AlterColumn<int>(
                name: "Id_promo",
                table: "Utilisateurs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Utilisateurs_Promos_Id_promo",
                table: "Utilisateurs",
                column: "Id_promo",
                principalTable: "Promos",
                principalColumn: "Id_promo",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
