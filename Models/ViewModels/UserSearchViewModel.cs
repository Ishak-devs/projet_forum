namespace Forum.Models
{
    public class UserSearchViewModel
    {

            public string SearchString { get; set; }
            public List<Utilisateurs> Eleves { get; set; }
            public List<Promos> Professeurs { get; set; }
        
    }
}
