using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Migrations
{
    /// <inheritdoc />
    public partial class update_eleve : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "prenom",
                table: "Elèves",
                newName: "Prenom");

            migrationBuilder.RenameColumn(
                name: "classe",
                table: "Elèves",
                newName: "Classe");

            migrationBuilder.RenameColumn(
                name: "MotDePasse",
                table: "Elèves",
                newName: "Password");

            migrationBuilder.AlterColumn<string>(
                name: "Classe",
                table: "Elèves",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Prenom",
                table: "Elèves",
                newName: "prenom");

            migrationBuilder.RenameColumn(
                name: "Classe",
                table: "Elèves",
                newName: "classe");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Elèves",
                newName: "MotDePasse");

            migrationBuilder.AlterColumn<string>(
                name: "classe",
                table: "Elèves",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
