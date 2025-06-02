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
        public double Coefficient { get; set; }

        public string Horaire { get; set; }

        [ForeignKey("Utilisateurs")]
        public int Id_utilisateur { get; set; }
        public virtual Utilisateurs Utilisateurs { get; set; }

        [ForeignKey("Classes")]
        public int Id_classe { get; set; }
        public virtual Classes Classes { get; set; }
    }
}
