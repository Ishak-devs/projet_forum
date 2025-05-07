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

        [ForeignKey("ProfesseurPrincipal")]
        public int? ProfesseurPrincipalId { get; set; }

        public virtual Utilisateurs? ProfesseurPrincipal { get; set; }

    }
}