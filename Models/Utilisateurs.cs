using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{
    public class Utilisateurs
    {
        [Key]
        public int Id_utilisateur { get; set; }

        public int? Id_promo { get; set; }

        [ForeignKey("Id_promo")]
        public virtual Promos? Promos { get; set; }

        [StringLength(70)]
        public string Nom { get; set; }

        [StringLength(50)]
        public string Prenom { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Motdepasse { get; set; }

        [StringLength(15)]
        public string Role { get; set; }

        [ForeignKey("Etablissements")]
        public int? Id_etablissement { get; set; }

        public virtual Etablissements? Etablissements { get; set; }

        [StringLength(30)]
        public string? Section { get; set; }

    }
}