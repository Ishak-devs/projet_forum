using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Forum.Models;

namespace WebApplication2.Models
{
    public class Alertes
    {
        [Key]
        public int Id_alerte { get; set; } 

        [ForeignKey("User")]
        public int Id_utilisateur { get; set; }

        [ForeignKey("Classe")]
        public int Id_promo { get; set; }

        public string Message { get; set; }

        public string Category { get; set; }

        public virtual Utilisateurs User { get; set; }

        public virtual Classes Classe { get; set; }
    }
}