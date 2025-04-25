using System.Data.Entity;
using Forum.Models;
using Microsoft.AspNetCore.SignalR;

namespace Forum.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChatHub(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task SendMessage(string message, int promoId)
        {
            var userId = _httpContextAccessor.HttpContext?.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId)) return;

            var user = await _context.Utilisateurs.FindAsync(int.Parse(userId));
            if (user == null) return;

            var promoExists = await _context.Promos.AnyAsync(p => p.Id_promo == promoId);
            if (!promoExists) return;

            var chatMessage = new Messages
            {
                Id_utilisateur = user.Id_utilisateur,
                Message = message,
                Id_promo = promoId,
                Date_message = DateTime.Now
            };

            _context.Messages.Add(chatMessage);
            await _context.SaveChangesAsync();

            await Clients.Group(promoId.ToString())
                .SendAsync("ReceiveMessage", user.Prenom, message, chatMessage.Date_message);
        }

        public async Task LoadMessages(int promoId)
        {
            var messages = await _context.Messages
                .Where(m => m.Id_promo == promoId)
                .OrderBy(m => m.Date_message)
                .Select(m => new
                {
                    SenderName = _context.Utilisateurs
                        .FirstOrDefault(u => u.Id_utilisateur == m.Id_utilisateur).Prenom,
                    m.Message,
                    m.Date_message
                })
                .ToListAsync();

            await Clients.Caller.SendAsync("MessagesLoaded", messages);
        }

        public async Task JoinPromoGroup(int promoId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, promoId.ToString());
            await LoadMessages(promoId); 
        }

        public async Task LeavePromoGroup(int promoId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, promoId.ToString());
        }
    }
}