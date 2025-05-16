using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{
    public class Alertes
    {
        [Key]
        public int Id_alerte { get; set; }

        [StringLength(100)]
        public string Message { get; set; }

        [ForeignKey("User")]
        public int Id_utilisateur { get; set; }

        [ForeignKey("Classe")]
        public int Id_classe { get; set; }

        public virtual Utilisateurs User { get; set; }
        public virtual Classes Classe { get; set; }


    }
}