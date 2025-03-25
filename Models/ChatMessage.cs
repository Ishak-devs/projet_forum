using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{
    public class ChatMessage
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(DetailsClasse))]
        public int Id_details_classe { get; set; }
        public virtual Details_classe DetailsClasse { get; set; }
        public int SenderId { get; set; }

        [Required, StringLength(2000)]
        public string Content { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}