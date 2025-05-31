using Forum.Models;

public class ClasseProf
{
    public int ClasseId { get; set; }
    public Classes Classe { get; set; }

    public int ProfId { get; set; }
    public Utilisateurs Prof { get; set; }
}
