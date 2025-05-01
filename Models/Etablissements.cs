using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{
    public class Etablissements
    {
        [Key]
        public int Id_etablissement { get; set; }
        public string type_etablissement { get; set; }
        public int numero_rue { get; set; }
        public string nom_rue { get; set; }
        public int code_postal { get; set; }
        public string Region { get; set; }
        public string ville { get; set; }
        public string nom_etablissement { get; set; }

    }
}