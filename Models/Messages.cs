using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{
    public class Messages
    {
        // Définition de la clé primaire
        [Key]
        public int Id_message { get; set; }

        [ForeignKey("Utilisateurs")]
        public int Id_utilisateur { get; set; }
        public virtual Utilisateurs Utilisateurs { get; set; }

        [ForeignKey("Classes")]
        public int Id_classe { get; set; }
        public virtual Classes Classes { get; set; }
        public string Message { get; set; }

        public DateTime Date_message { get; set; } = DateTime.UtcNow;
    }
}