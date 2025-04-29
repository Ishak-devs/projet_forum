using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class Classes
    {
        [Key]
        public int Classid { get; set; }

        [StringLength(100)]
        public string Nom_classe { get; set; }

    }
}