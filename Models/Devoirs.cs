using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{
    public class Devoirs
    {
        [Key]
        public int Id_devoir { get; set; }

        public string Intitulé { get; set; }

        public DateTime Date_devoir { get; set; } = DateTime.Now;

        public string? FichierPath { get; set; }


        [ForeignKey("Modules")]
        public int Id_module { get; set; }

        public virtual Modules Modules { get; set; }

        [ForeignKey("Utilisateurs")]
        public int Id_utilisateur { get; set; }

        public virtual Utilisateurs Utilisateurs { get; set; }
    }
}
