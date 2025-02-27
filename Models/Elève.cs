using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class Elève 
    {
        [Key]
        public int Id { get; set; } 


        [Required(ErrorMessage = "Le nom est obligatoire")]
        [StringLength(70)]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le Prénom est obligatoire")]
        [StringLength(50)]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "L'email est obligatoire")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le mot de passe est obligatoire")]
        [StringLength(100)]
        public string Password { get; set; }
        

        [StringLength(50)]
        public string Classe { get; set; }
        
        public DateTime DateInscription { get; set; } = DateTime.Now;
    }
}
