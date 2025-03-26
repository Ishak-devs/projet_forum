using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Http;
using Forum.Models;
using Microsoft.EntityFrameworkCore;

namespace Forum.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext contextget;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChatHub(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            contextget = context;
            _httpContextAccessor = httpContextAccessor; 
        }

        public async Task SendMessage(string message)
        {
            var httpContext = Context.GetHttpContext(); // Récupère le HttpContext
            var eleveId = httpContext.Session.GetString("Eleve_id");
            var profId = httpContext.Session.GetString("Prof_id");

            string nomUtilisateur = "Anonyme";
            if (!string.IsNullOrEmpty(eleveId))
            {
                var eleve = await contextget.Eleves.FindAsync(int.Parse(eleveId));
                nomUtilisateur = eleve?.Nom ?? "Élève inconnu";
            }
            else if (!string.IsNullOrEmpty(profId))
            {
                var professeur = await contextget.Professeurs.FindAsync(int.Parse(profId));
                nomUtilisateur = professeur?.Nom ?? "Professeur inconnu";
            }

            await Clients.All.SendAsync("ReceiveMessage", nomUtilisateur, message);
        }
    }
}