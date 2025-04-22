using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class Admin
    {
        [Key]
        public int Id_admin { get; set; }

        [Required]
        [StringLength(70)]
        public string Prenom { get; set; }

        [Required]
        [StringLength(70)]
        public string Nom { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }
    }
}
