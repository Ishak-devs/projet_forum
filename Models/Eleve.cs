using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Forum.Models
{
    public class Eleve 
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
        [EmailAddress(ErrorMessage = "format d'email invalide")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le mot de passe est obligatoire")]
        [DataType(DataType.Password)]
        [StringLength(100)]
        public string Password { get; set; }

        [BindNever]
        public DateTime DateInscription { get; set; } = DateTime.Now;
    }
}