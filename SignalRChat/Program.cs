
using Microsoft.EntityFrameworkCore;
using SignalRChat.Context;
using SignalRChat.Hubs;



namespace SignalRChat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ChatDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("ChatConnection"));
            });
            builder.Services.AddSignalR();
            var app = builder.Build();

            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/chat");
            });
     

            app.Run();
        }
    }
}
