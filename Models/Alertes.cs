
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Forum.Models;



namespace Forum.Models
{
    public class Alertes
    {
        [Key]

        public int Id_alerte { get; set; }

        public string TypeAlerte { get; set; }
        public string Titre { get; set; }

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