using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{
    public class Classe
    {
        [Key]
        public int Id { get; set; }
        public string classe { get; set; }

        [ForeignKey("Professeur")]
        public int id_professeur { get; set; }
        public virtual Professeur Professeur { get; set; }

        [ForeignKey("Eleve")]
        public int id_eleve { get; set; }
        public virtual Eleve Eleve { get; set; }

    }
}