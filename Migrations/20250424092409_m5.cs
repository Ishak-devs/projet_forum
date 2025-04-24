using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Migrations
{
    /// <inheritdoc />
    public partial class m5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id_promo",
                table: "Modules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Modules_Id_promo",
                table: "Modules",
                column: "Id_promo");

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Promos_Id_promo",
                table: "Modules",
                column: "Id_promo",
                principalTable: "Promos",
                principalColumn: "Id_promo",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Promos_Id_promo",
                table: "Modules");

            migrationBuilder.DropIndex(
                name: "IX_Modules_Id_promo",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "Id_promo",
                table: "Modules");
        }
    }
}
