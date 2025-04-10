using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{
    public class Amis
    {
        [Key]
        public int Id { get; set; }
        public int Id_user { get; set; }
        public int AmisId { get; set; }
        public bool Accepted { get; set; }
    }
}