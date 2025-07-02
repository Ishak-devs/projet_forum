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

        public string Horaire { get; set; }

        [ForeignKey("Promos")]
        public int Id_classe { get; set; }
        public virtual Promos Promos { get; set; }
    }
}
