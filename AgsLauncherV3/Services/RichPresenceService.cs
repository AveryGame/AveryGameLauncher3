using System;
using DiscordRPC;
using DiscordRPC.Message;
using DiscordRPC.Logging;

namespace AgsLauncherV3.Services
{
    internal class RichPresenceService
    {
        public static DiscordRpcClient client;
        public static void Handler()
        {
            Console.WriteLine("Loaded all page components and finished animation.");
            client = new DiscordRpcClient("1023932512612925501");
            client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };
            client.OnReady += (sender, e) =>
            {
                Console.WriteLine("[LOGRPC]: Received Ready from user {0}", e.User.Username);
            };
            client.OnPresenceUpdate += (sender, e) =>
            {
                Console.WriteLine("[LOGRPC]: Received Update! {0}", e.Presence);
            };
            client.Initialize();
            client.SetPresence(new RichPresence()
            {
                Details = "Browsing the library...",
                State = "",
                Assets = new Assets()
                {
                    LargeImageKey = "ag2cover",
                    LargeImageText = "PD-Hana..BuildDate",
                    SmallImageKey = ""
                }
            });
            client.UpdateStartTime(DateTime.UtcNow);
            client.OnConnectionFailed += delegate (object sender, ConnectionFailedMessage e)
            {
                client.Dispose();
            };
        }
    }
}
