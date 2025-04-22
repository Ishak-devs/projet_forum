using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class Evaluations
    {
        [Key]
        public int Id_evaluation { get; set; }
        public DateTime Date_evaluation { get; set; }
        public string Theme_evaluation { get; set; }
        public Decimal? Note_evaluation { get; set; }
    }
}
