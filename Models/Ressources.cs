using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{
    public class Ressources //
    {
        [Key]
        public int Id_ressources { get; set; }

        public string Intitulé { get; set; }

        public DateTime Date_devoir { get; set; } = DateTime.Now;

        public string? FichierPath { get; set; }


        [ForeignKey("Utilisateurs")]
        public int Id_utilisateur { get; set; }

        public virtual Utilisateurs Utilisateurs { get; set; }


   

    }
}
