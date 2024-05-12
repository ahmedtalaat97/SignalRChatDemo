using Microsoft.EntityFrameworkCore;
using SignalRChat.Models;

namespace SignalRChat.Context
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }    
    }
}
