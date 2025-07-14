using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{
    public class Promos
    {
        [Key]
        public int Id_promos { get; set; }

        [StringLength(100)]
        public string Nom_promos { get; set; }

        public virtual ICollection<Utilisateurs> Utilisateurs { get; set; }

        public int? Id_etablissement { get; set; }

        [ForeignKey("Id_etablissement")]
        public virtual Etablissements Etablissement { get; set; }
    }
}