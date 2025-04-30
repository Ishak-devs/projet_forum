

using Forum.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Forum.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<ChatHub> _logger;

        public ChatHub(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, ILogger<ChatHub> logger)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _logger.LogInformation("ChatHub instancié !");
        }

        public async Task SendMessage(string message, int Classid)
        {
            _logger.LogInformation("SendMessage appelé avec message : {Message}, Classid : {Classid}", message, Classid);
            var userId = _httpContextAccessor.HttpContext?.Session.GetInt32("UserId");

            var user = await _context.Utilisateurs.FindAsync(userId);
            if (user == null) return;

            var promoExists = await _context.Classes.AnyAsync(p => p.Id_classe == Classid);
            if (!promoExists) return;

            var chatMessage = new Messages
            {
                Id_utilisateur = user.Id_utilisateur,
                Message = message,
                Id_classe = Classid,
                Date_message = DateTime.Now
            };

            _context.Messages.Add(chatMessage);
            await _context.SaveChangesAsync();

            await Clients.Group(Classid.ToString())
                .SendAsync("ReceiveMessage", user.Prenom, message, chatMessage.Date_message);
        }

        public Task Message_send_and_echo_to_client(int Classid)
        {
            try
            {
                _logger.LogInformation($"Chargement des messages pour la promo {Classid}...");

                var messages = _context.Messages
                    .Where(m => m.Id_classe == Classid)
                    .OrderBy(m => m.Date_message)
                    .Select(m => new
                    {
                        SenderName = _context.Utilisateurs
                            .Where(u => u.Id_utilisateur == m.Id_utilisateur)
                            .Select(u => u.Prenom)
                            .FirstOrDefault(),
                        m.Message,
                        m.Date_message
                    })
                    .ToList();

                _logger.LogInformation($"Messages chargés : {messages.Count}");
                return Clients.Caller.SendAsync("MessagesLoaded", messages);
            }
            catch (Exception e)
            {
                _logger.LogError($"Erreur lors du chargement des messages: {e.Message}");
                throw;
            }
        }


    }
}