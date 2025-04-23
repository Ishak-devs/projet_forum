
using System.Data.Entity;
using Forum.Models;
using Microsoft.AspNetCore.SignalR;

namespace Forum.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext contextget;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public ChatHub(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            contextget = context; //Recuperation du context
            _httpContextAccessor = httpContextAccessor; //Récuperation de httpcontext
        }

        public async Task SendMessage(string message, int classId) //Création objet pour l'envoi des messages
        {
            var httpContext = Context.GetHttpContext(); // Stockage de httpcontext 
            var eleveId = httpContext.Session.GetString("Eleve_id"); //Stockage de l'id eleve
            var profId = httpContext.Session.GetString("Prof_id"); //Stockage de prof id

            string nomUtilisateur = "Anonyme"; //Définition pour réutilisation
            int senderId = 0;//Définition pour réutilisation


            if (!string.IsNullOrEmpty(eleveId)) //Si id eleve connu
            {
                var eleve = await contextget.Utilisateurs.FindAsync(int.Parse(eleveId)); //recuperation des info de l'eleve
                nomUtilisateur = eleve?.Prenom ?? "Élève inconnu"; //Si un nom est connu on l'affiche sinon inconnu
                senderId = int.Parse(eleveId); //SenderId ici deviens eleveId
            }
            else if (!string.IsNullOrEmpty(profId))
            {
                var professeur = await contextget.Utilisateurs.FindAsync(int.Parse(profId));
                nomUtilisateur = professeur?.Prenom ?? "Professeur inconnu";
                senderId = int.Parse(profId);
            }

            var chatMessage = new Messages //Ajout dans la table de chat
            {
                Id_utilisateur = senderId,
                Message = message
            };

            contextget.Messages.Add(chatMessage); //Methode add pour ajouter
            await contextget.SaveChangesAsync(); //Sauvegarde dans la base de données

            await Clients.All.SendAsync("ReceiveMessage", nomUtilisateur, message); //Affichage des messages
        }
        public async Task LoadMessages(int promoid) //chargement des messages
        {
            var messages = await contextget.Messages
                .Where(m => m.Id_promo == promoid) //Where id de details classe correspond a l'id de la classe
                .OrderBy(m => m.Date_message) //Affichage des messages par dates recente
                .Select(m => new
                {
                    SenderName = contextget.Utilisateurs.Any(e => e.Id_utilisateur == m.Id_utilisateur)
                        ? contextget.Utilisateurs.FirstOrDefault(e => e.Id_utilisateur == m.Id_utilisateur).Prenom
                        : contextget.Utilisateurs.FirstOrDefault(p => p.Id_utilisateur == m.Id_utilisateur).Prenom,
                    m.Message,
                    m.Date_message
                })
                .ToListAsync(); //Afficher sous forme de liste

            foreach (var msg in messages) 
            {
                await Clients.Caller.SendAsync("ReceiveMessage", msg.SenderName, msg.Message, msg.Date_message); //Affichage 
            }
        }
    }
}