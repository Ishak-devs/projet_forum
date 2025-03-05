using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class ClasseViewModel
    {
        [Required(ErrorMessage = "La classe est obligatoire")]
        public string Classe { get; set; }

        [Required(ErrorMessage = "veuillez ajouter des eleves")]
        public int id_eleve { get; set; }
    }
}
