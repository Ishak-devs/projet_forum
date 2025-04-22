using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Forum.Models
{
    public class Utilisateurs 
    {
        [Key]
        public int Id_utilisateur { get; set; } 

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

    }
}