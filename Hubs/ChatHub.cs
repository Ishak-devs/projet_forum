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
        }

        public async Task SendMessage(string message, int classId)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.Session.GetInt32("UserId");
                if (userId == null) return;

                var user = await _context.Utilisateurs.FindAsync(userId);
                if (user == null) return;

                var newMessage = new Messages
                {
                    Id_utilisateur = user.Id_utilisateur,
                    Message = message,
                    Id_classe = classId,
                    Date_message = DateTime.Now
                };

                _context.Messages.Add(newMessage);
                await _context.SaveChangesAsync();

                await Clients.Group(classId.ToString())
                    .SendAsync("ReceiveMessage", user.Prenom, message, newMessage.Date_message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in SendMessage: {ex.Message}");
                throw;
            }
        }

        public async Task LoadInitialMessages(int classId)
        {
            var messages = await _context.Messages
                .Where(m => m.Id_classe == classId)
                .OrderBy(m => m.Date_message)
                .Select(m => new
                {
                    SenderName = _context.Utilisateurs
                        .Where(u => u.Id_utilisateur == m.Id_utilisateur)
                        .Select(u => u.Prenom)
                        .FirstOrDefault(),
                    Message = m.Message,
                    DateMessage = m.Date_message
                })
                .ToListAsync();

            await Clients.Caller.SendAsync("MessagesLoaded", messages);
        }

        public override async Task OnConnectedAsync()
        {
            var userId = _httpContextAccessor.HttpContext?.Session.GetInt32("UserId");
            if (userId.HasValue)
            {
                var user = await _context.Utilisateurs.FindAsync(userId.Value);
                if (user?.Id_classe != null)
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, user.Id_classe.Value.ToString());
                }
            }
            await base.OnConnectedAsync();
        }
    }
}