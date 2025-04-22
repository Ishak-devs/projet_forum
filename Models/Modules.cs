using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Forum.Models
{
    public class Modules
    {
        [Key]
        public int Id_module { get; set; }

        public string theme_module { get; set; }
      
        public DateTime Date_module { get; set; }
    }
}
