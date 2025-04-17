using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{
    public class ChatMessage
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Classe))]
        public int Id_classe { get; set; }
        public virtual Classe Classe { get; set; }
        public int SenderId { get; set; }

        [Required, StringLength(2000)]
        public string Content { get; set; }
        //Date time
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}