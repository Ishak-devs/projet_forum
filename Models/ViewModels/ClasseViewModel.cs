using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Forum.Models
{
    public class ClasseViewModel
    {
        [Required(ErrorMessage = "La classe est obligatoire")]
        public string Nom_classe { get; set; }

        public SelectList Eleves { get; set; }

        public int Id_user { get; set; }

        public List <int> Eleveschoisis {  get; set; } //Element de stockage temporaire avant d'ajouter les élèves au groupe de classe.
    }
}
