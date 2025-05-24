using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{
    public class Notes
    {
        [Key]
        public int Id_note { get; set; }

        public double Note { get; set; }

        public DateTime Date_note { get; set; } = DateTime.Now;

        [ForeignKey("Modules")]
        public int Id_module { get; set; }

        public virtual Modules Modules { get; set; }

        [ForeignKey("Utilisateurs")]
        public int Id_utilisateur { get; set; }

        public virtual Utilisateurs Utilisateurs { get; set; }
    }
}