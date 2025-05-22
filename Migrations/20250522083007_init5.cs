using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Migrations
{
    /// <inheritdoc />
    public partial class init5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Devoirs",
                columns: table => new
                {
                    Id_devoir = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Intitulé = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Date_devoir = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Id_module = table.Column<int>(type: "int", nullable: false),
                    Id_utilisateur = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devoirs", x => x.Id_devoir);
                    table.ForeignKey(
                        name: "FK_Devoirs_Modules_Id_module",
                        column: x => x.Id_module,
                        principalTable: "Modules",
                        principalColumn: "Id_module",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Devoirs_Utilisateurs_Id_utilisateur",
                        column: x => x.Id_utilisateur,
                        principalTable: "Utilisateurs",
                        principalColumn: "Id_utilisateur",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Devoirs_Id_module",
                table: "Devoirs",
                column: "Id_module");

            migrationBuilder.CreateIndex(
                name: "IX_Devoirs_Id_utilisateur",
                table: "Devoirs",
                column: "Id_utilisateur");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Devoirs");
        }
    }
}
