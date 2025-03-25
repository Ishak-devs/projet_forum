using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Http;

namespace Forum.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext contextget;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ChatHub(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            contextget = context;
           
        }

        public async Task SendMessage(string message)
        {
            var eleveId = _httpContextAccessor.HttpContext?.Session.GetString("Eleve_id");
            var profId = _httpContextAccessor.HttpContext?.Session.GetString("Prof_id");

            string nomUtilisateur = "Anonyme";

            await Clients.All.SendAsync("ReceiveMessage", nomUtilisateur, message);
        }

    }

}