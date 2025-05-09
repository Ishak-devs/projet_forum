using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{
    public class Evaluations
    {
        [Key]
        public int Id_evaluation { get; set; }

        public DateTime Date_evaluation { get; set; }

        public string Theme_evaluation { get; set; }

        public Decimal? Note_evaluation { get; set; }

        [ForeignKey("User")]
        public int Id_utilisateur { get; set; } 

        [ForeignKey("Module")]
        public int Id_module { get; set; }  

        public virtual Utilisateurs User { get; set; }

        public virtual Modules Module { get; set; }
    }
}
