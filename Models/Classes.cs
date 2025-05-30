using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{
    public class Classes
    {
        [Key]
        public int Id_classe { get; set; }

        [StringLength(100)]
        public string Nom_classe { get; set; }

        public virtual ICollection<Utilisateurs> Utilisateurs { get; set; }



    }
}