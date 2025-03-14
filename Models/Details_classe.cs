using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Forum.Models
{
    public class Details_classe
    {
        [Key]
        public int Id_details_classe { get; set; }

        [ForeignKey("Classe")]
        public int Id_classe { get; set; }
        public virtual Classe Classe { get; set; }

        [ForeignKey("Professeur")]
        public int id_professeur { get; set; }
        public virtual Professeur Professeur { get; set; }

        [ForeignKey("Eleve")]
        public int id_eleve { get; set; }
        public virtual Eleve Eleve { get; set; }
    }
}
