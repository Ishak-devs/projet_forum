using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class Professeur
    {
        [Key]
        public int Id { get; set; }

        [StringLength(70)]
        public string Nom { get; set; }

        [StringLength(50)]
        public string Prenom { get; set; }

        [StringLength(30)]
        public string Matiere { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

    }
}