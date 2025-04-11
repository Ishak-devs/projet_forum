using Forum.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Amis
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("EleveDemandeur")]
    public int EleveDemandeurId { get; set; }
    public virtual Eleve EleveDemandeur { get; set; }

    [ForeignKey("EleveAmi")]
    public int EleveAmiId { get; set; }
    public virtual Eleve EleveAmi { get; set; }

    public bool Accepted { get; set; }
}