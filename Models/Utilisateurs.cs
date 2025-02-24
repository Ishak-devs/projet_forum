using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class Utilisateur // Utilise le singulier pour le nom de la classe
    {
        [Key]
        public int Id { get; set; } // Clé primaire

        [Required]
        [StringLength(100)]
        public string Nom { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string MotDePasse { get; set; }

        public DateTime DateInscription { get; set; } = DateTime.Now;
    }
}
