using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Etablissements",
                columns: table => new
                {
                    Id_etablissement = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    type_etablissement = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    code_postal = table.Column<int>(type: "int", nullable: false),
                    Region = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    academie = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nom_etablissement = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etablissements", x => x.Id_etablissement);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Alerte",
                columns: table => new
                {
                    Id_alerte = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Message = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_utilisateur = table.Column<int>(type: "int", nullable: false),
                    Id_classe = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alerte", x => x.Id_alerte);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id_classe = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nom_classe = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProfesseurPrincipalId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id_classe);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    Id_module = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Theme_module = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Horaire = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_classe = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.Id_module);
                    table.ForeignKey(
                        name: "FK_Modules_Classes_Id_classe",
                        column: x => x.Id_classe,
                        principalTable: "Classes",
                        principalColumn: "Id_classe",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Utilisateurs",
                columns: table => new
                {
                    Id_utilisateur = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_classe = table.Column<int>(type: "int", nullable: true),
                    Nom = table.Column<string>(type: "varchar(70)", maxLength: 70, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Prenom = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Motdepasse = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Role = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_etablissement = table.Column<int>(type: "int", nullable: true),
                    Matiere = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Section = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateurs", x => x.Id_utilisateur);
                    table.ForeignKey(
                        name: "FK_Utilisateurs_Classes_Id_classe",
                        column: x => x.Id_classe,
                        principalTable: "Classes",
                        principalColumn: "Id_classe");
                    table.ForeignKey(
                        name: "FK_Utilisateurs_Etablissements_Id_etablissement",
                        column: x => x.Id_etablissement,
                        principalTable: "Etablissements",
                        principalColumn: "Id_etablissement");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Evaluations",
                columns: table => new
                {
                    Id_evaluation = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date_evaluation = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Theme_evaluation = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Note_evaluation = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Id_utilisateur = table.Column<int>(type: "int", nullable: false),
                    Id_module = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluations", x => x.Id_evaluation);
                    table.ForeignKey(
                        name: "FK_Evaluations_Modules_Id_module",
                        column: x => x.Id_module,
                        principalTable: "Modules",
                        principalColumn: "Id_module",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Evaluations_Utilisateurs_Id_utilisateur",
                        column: x => x.Id_utilisateur,
                        principalTable: "Utilisateurs",
                        principalColumn: "Id_utilisateur",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id_message = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_utilisateur = table.Column<int>(type: "int", nullable: false),
                    Id_classe = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Date_message = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id_message);
                    table.ForeignKey(
                        name: "FK_Messages_Classes_Id_classe",
                        column: x => x.Id_classe,
                        principalTable: "Classes",
                        principalColumn: "Id_classe",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Utilisateurs_Id_utilisateur",
                        column: x => x.Id_utilisateur,
                        principalTable: "Utilisateurs",
                        principalColumn: "Id_utilisateur",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Alerte_Id_classe",
                table: "Alerte",
                column: "Id_classe");

            migrationBuilder.CreateIndex(
                name: "IX_Alerte_Id_utilisateur",
                table: "Alerte",
                column: "Id_utilisateur");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_ProfesseurPrincipalId",
                table: "Classes",
                column: "ProfesseurPrincipalId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_Id_module",
                table: "Evaluations",
                column: "Id_module");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_Id_utilisateur",
                table: "Evaluations",
                column: "Id_utilisateur");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_Id_classe",
                table: "Messages",
                column: "Id_classe");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_Id_utilisateur",
                table: "Messages",
                column: "Id_utilisateur");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_Id_classe",
                table: "Modules",
                column: "Id_classe");

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateurs_Id_classe",
                table: "Utilisateurs",
                column: "Id_classe");

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateurs_Id_etablissement",
                table: "Utilisateurs",
                column: "Id_etablissement");

            migrationBuilder.AddForeignKey(
                name: "FK_Alerte_Classes_Id_classe",
                table: "Alerte",
                column: "Id_classe",
                principalTable: "Classes",
                principalColumn: "Id_classe",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Alerte_Utilisateurs_Id_utilisateur",
                table: "Alerte",
                column: "Id_utilisateur",
                principalTable: "Utilisateurs",
                principalColumn: "Id_utilisateur",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Utilisateurs_ProfesseurPrincipalId",
                table: "Classes",
                column: "ProfesseurPrincipalId",
                principalTable: "Utilisateurs",
                principalColumn: "Id_utilisateur");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Utilisateurs_Classes_Id_classe",
                table: "Utilisateurs");

            migrationBuilder.DropTable(
                name: "Alerte");

            migrationBuilder.DropTable(
                name: "Evaluations");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Utilisateurs");

            migrationBuilder.DropTable(
                name: "Etablissements");
        }
    }
}
