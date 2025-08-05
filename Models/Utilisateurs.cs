using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{
    public class Utilisateurs
    {
        [Key]
        public int Id_utilisateur { get; set; }

        public int? Id_promos { get; set; }

        [ForeignKey("Id_promos")]
        public virtual Promos? Promos { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire")]
        [MinLength(2, ErrorMessage = "Le nom doit contenir au moins 2 caractères.")]
        [StringLength(70)]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le prénom est obligatoire")]
        [StringLength(50)]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "L'email est obligatoire")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le mot de passe est obligatoire")]
        [MinLength(8, ErrorMessage = "Le mot de passe doit contenir au moins 8 caractères.")]
        [StringLength(100)]
        public string Motdepasse { get; set; }

        [StringLength(15)]
        public string Role { get; set; }

        public int? Id_etablissement { get; set; }

        [ForeignKey("Id_etablissement")]
        public virtual Etablissements? Etablissements { get; set; }

        [StringLength(30)]
        public string? Matiere { get; set; }

        public bool bloquage { get; set; } =  false;

        public DateTime? bloquage_temps { get; set; }
    }
}
