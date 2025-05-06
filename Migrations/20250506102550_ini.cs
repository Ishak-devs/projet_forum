using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Migrations
{
    /// <inheritdoc />
    public partial class ini : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Principal",
                table: "Utilisateurs");

            migrationBuilder.AddColumn<int>(
                name: "ProfesseurPrincipalId",
                table: "Classes",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfesseurPrincipalId",
                table: "Classes");

            migrationBuilder.AddColumn<sbyte>(
                name: "Principal",
                table: "Utilisateurs",
                type: "tinyint(30)",
                maxLength: 30,
                nullable: true);
        }
    }
}
