using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{
    public class Classe
    {
        [Key]
        public int Id { get; set; }
        public string classe { get; set; }

    }
}