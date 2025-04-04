namespace Forum.Models
{
    public class UserSearchViewModel
    {

            public string SearchString { get; set; }
            public List<Eleve> Eleves { get; set; }
            public List<Professeur> Professeurs { get; set; }
        
    }
}
