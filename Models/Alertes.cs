<<<<<<< HEAD
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
=======
﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Forum.Models;

>>>>>>> 444c98a63715962a62c856341a626d674a925ce1

namespace Forum.Models
{
    public class Alertes
    {
        [Key]
<<<<<<< HEAD
        public int Id_alerte { get; set; }

        [StringLength(100)]
        public string Message { get; set; }
=======
        public int Id_alerte { get; set; } 
>>>>>>> 444c98a63715962a62c856341a626d674a925ce1

        [ForeignKey("User")]
        public int Id_utilisateur { get; set; }

        [ForeignKey("Classe")]
<<<<<<< HEAD
        public int Id_classe { get; set; }

        public virtual Utilisateurs User { get; set; }
        public virtual Classes Classe { get; set; }


=======
        public int Id_promo { get; set; }

        public string Message { get; set; }

        public string Category { get; set; }

        public virtual Utilisateurs User { get; set; }

        public virtual Classes Classe { get; set; }
>>>>>>> 444c98a63715962a62c856341a626d674a925ce1
    }
}