using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Forum.Models
{
    public class SignupViewModel
    {

        [Required(ErrorMessage = "Le nom est obligatoire")]
        [StringLength(70, MinimumLength =2, ErrorMessage ="Le prénom doit contenir au moins 2 caractères.")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le Prénom est obligatoire")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Le prénom doit contenir au moins 2 caractères.")]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "L'email est obligatoire")]
        [EmailAddress(ErrorMessage = "format d'email invalide")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le mot de passe est obligatoire")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Le Mot de passe doit contenir au moins 2 caractères.")]
        public string Password { get; set; }
    }
}