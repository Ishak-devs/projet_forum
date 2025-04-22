using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{
    public class Messages
    {
        [Key]
        public int Id_message { get; set; }

        public string Message { get; set; }

        public DateTime Date_message { get; set; } = DateTime.UtcNow;
    }
}