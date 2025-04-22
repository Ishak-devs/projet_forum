using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class Promos
    {
        [Key]
        public int Id_promo { get; set; }

        [StringLength(100)]
        public string Nom_promo { get; set; }

    }
}