using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{
    public class Etablissements
    {
        [Key]
        public int Id_etablissement { get; set; }

        [Required]
        public int code_postal { get; set; }

        [Required]
        public string Region { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "L'académie doit contenir au moins 5 caractères.")]
        public string academie { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Le nom de l'établissement doit contenir au moins 2 caractères.")]
        public string nom_etablissement { get; set; }
    }
}
