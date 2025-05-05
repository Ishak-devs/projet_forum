using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Forum.Models
{
    public class Modules
    {
        [Key]
        public int Id_module { get; set; }

        public string Theme_module { get; set; }
      
        public DateTime Date_module { get; set; }

        public DateTime debut_cours { get; set; }

        public DateTime fin_cours { get; set; }

        [ForeignKey("Classes")]
        public int Id_classe { get; set; }
        public virtual Classes Classes { get; set; }
    }
}
