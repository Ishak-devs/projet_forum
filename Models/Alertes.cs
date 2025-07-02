
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

            public string Titre { get; set; }

            [StringLength(100)]
            public string Message { get; set; }

        public DateTime date_alerte { get; set; } = DateTime.Now;

        [ForeignKey("Promos")]
        public int Id_classe { get; set; }
        public virtual Promos Promos { get; set; }
    }



    }