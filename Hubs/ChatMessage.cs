
namespace Forum.Hubs
{
    internal class ChatMessage
    {
        public int Id_classe { get; set; }
        public int SenderId { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
    }
}