using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{
    public class Etablissements
    {
        [Key]
        public int Id_etablissement { get; set; }
        public string type_etablissement { get; set; }
        public string adresse { get; set; }
        public int code_postal { get; set; }
        public string Region { get; set; }
        public string commune { get; set; }
        public string nom_etablissement { get; set; }

    }
}