using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Forum.Models
{
    public class Obtention
    {
        public int Id_module { get; set; }

        [ForeignKey("Module")]
        public virtual Module Module { get; set; }

        public int Id_notes { get; set; }

        [ForeignKey("Notes")]
        public virtual Evaluations Notes { get; set; }

        public int id_user { get; set; }

        [ForeignKey("Utilisateurs")]
        public virtual Utilisateurs Utilisateurs { get; set; }

        public bool Admis { get; set; }
    }
}
