using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class Utilisateur 
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        [StringLength(70)]
        public string Nom { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string MotDePasse { get; set; }

        public DateTime DateInscription { get; set; } = DateTime.Now;
    }
}
