using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Forum.Models
{
    public class AmisViewModel
    {
        public int Id_user { get; set; }
        public int AmisId { get; set; }

        public List<int> Amischoisis { get; set; } //Element de stockage temporaire avant d'ajouter les élèves au groupe de classe.
    }
}
