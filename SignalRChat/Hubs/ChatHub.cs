using Microsoft.AspNetCore.SignalR;
using SignalRChat.Context;
using SignalRChat.Models;

namespace SignalRChat.Hubs
{
    public class ChatHub:Hub
    {
        private readonly ILogger<ChatHub> _logger;
        private readonly ChatDbContext _context;

        public ChatHub(ILogger<ChatHub> logger, ChatDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        public async Task Send (string userName ,string message)
        {
            await Clients.Others.SendAsync ("ReciveMessage",userName, message);

            var msg = new Message
            {
                UserName = userName,
                MessageText = message

            };
            await _context.AddAsync(msg);   
            await _context.SaveChangesAsync();

        }

        public async Task JoinGroup(string groupName,string userName)
        {
            await Groups.AddToGroupAsync (Context.ConnectionId, groupName);

            await Clients.OthersInGroup(groupName).SendAsync("NewMemberJoin",groupName,userName);

            _logger.LogInformation(Context.ConnectionId);

        }


        public async Task SendToGroup(string groupName, string sender , string message)
        {
            await Clients.OthersInGroup(groupName).SendAsync("ReciveMessageFromGroup", sender, message);

            var msg = new Message
            {
                UserName = sender,
                MessageText = message

            };
            await _context.AddAsync(msg);
            await _context.SaveChangesAsync();

        }





    }
}
