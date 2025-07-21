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
                name: "Promos",
                columns: table => new
                {
                    Id_promos = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nom_promos = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_etablissement = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promos", x => x.Id_promos);
                    table.ForeignKey(
                        name: "FK_Promos_Etablissements_Id_etablissement",
                        column: x => x.Id_etablissement,
                        principalTable: "Etablissements",
                        principalColumn: "Id_etablissement");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Alertes",
                columns: table => new
                {
                    Id_alerte = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Titre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Message = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    date_alerte = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Id_promos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alertes", x => x.Id_alerte);
                    table.ForeignKey(
                        name: "FK_Alertes_Promos_Id_promos",
                        column: x => x.Id_promos,
                        principalTable: "Promos",
                        principalColumn: "Id_promos",
                        onDelete: ReferentialAction.Cascade);
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
                    Id_promos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.Id_module);
                    table.ForeignKey(
                        name: "FK_Modules_Promos_Id_promos",
                        column: x => x.Id_promos,
                        principalTable: "Promos",
                        principalColumn: "Id_promos",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Utilisateurs",
                columns: table => new
                {
                    Id_utilisateur = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_promos = table.Column<int>(type: "int", nullable: true),
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
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateurs", x => x.Id_utilisateur);
                    table.ForeignKey(
                        name: "FK_Utilisateurs_Etablissements_Id_etablissement",
                        column: x => x.Id_etablissement,
                        principalTable: "Etablissements",
                        principalColumn: "Id_etablissement");
                    table.ForeignKey(
                        name: "FK_Utilisateurs_Promos_Id_promos",
                        column: x => x.Id_promos,
                        principalTable: "Promos",
                        principalColumn: "Id_promos");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Evaluations",
                columns: table => new
                {
                    Id_evaluation = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Note = table.Column<double>(type: "double", nullable: false),
                    Date_note = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Id_module = table.Column<int>(type: "int", nullable: false),
                    Id_utilisateur = table.Column<int>(type: "int", nullable: false)
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
                        name: "FK_Messages_Promos_Id_classe",
                        column: x => x.Id_classe,
                        principalTable: "Promos",
                        principalColumn: "Id_promos",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Utilisateurs_Id_utilisateur",
                        column: x => x.Id_utilisateur,
                        principalTable: "Utilisateurs",
                        principalColumn: "Id_utilisateur",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Ressources",
                columns: table => new
                {
                    Id_ressources = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Intitulé = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Date_devoir = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FichierPath = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_utilisateur = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ressources", x => x.Id_ressources);
                    table.ForeignKey(
                        name: "FK_Ressources_Utilisateurs_Id_utilisateur",
                        column: x => x.Id_utilisateur,
                        principalTable: "Utilisateurs",
                        principalColumn: "Id_utilisateur",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Alertes_Id_promos",
                table: "Alertes",
                column: "Id_promos");

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
                name: "IX_Modules_Id_promos",
                table: "Modules",
                column: "Id_promos");

            migrationBuilder.CreateIndex(
                name: "IX_Promos_Id_etablissement",
                table: "Promos",
                column: "Id_etablissement");

            migrationBuilder.CreateIndex(
                name: "IX_Ressources_Id_utilisateur",
                table: "Ressources",
                column: "Id_utilisateur");

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateurs_Id_etablissement",
                table: "Utilisateurs",
                column: "Id_etablissement");

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateurs_Id_promos",
                table: "Utilisateurs",
                column: "Id_promos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alertes");

            migrationBuilder.DropTable(
                name: "Evaluations");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Ressources");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "Utilisateurs");

            migrationBuilder.DropTable(
                name: "Promos");

            migrationBuilder.DropTable(
                name: "Etablissements");
        }
    }
}
