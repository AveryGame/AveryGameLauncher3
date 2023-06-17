using System;
using System.Windows;
using DiscordRPC;
using DiscordRPC.Message;
using DiscordRPC.Logging;

namespace AveryGameLauncher3.Services
{
    internal class RichPresenceService
    {
        public static DiscordRpcClient client;
        public static void Handler()
        {
            Console.WriteLine("Loaded all page components and finished animation.");
            client = new DiscordRpcClient("1023932512612925501");
            client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };
            client.OnReady += (sender, msg) =>
            {
                Application.Current.Dispatcher.Invoke(OnRichPresenceConnectionSuccess, System.Windows.Threading.DispatcherPriority.ContextIdle);
                PfpUrl = client.CurrentUser.GetAvatarURL(User.AvatarFormat.PNG, User.AvatarSize.x32);
                bHasRpc = true;
            };
            client.OnPresenceUpdate += (sender, e) =>
            {
                Console.WriteLine("[AVGL3Debug]: Recieved presence update from Discord.");
            };
            client.OnConnectionFailed += (sender, e) =>
            {
                bHasRpc = false;
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
                },
                Buttons = new DiscordRPC.Button[]
                {
                    new DiscordRPC.Button()
                    {
                        Label = "Download",
                        Url = "https://trollface.dk"
                        
                    }
                }
            });
            client.UpdateStartTime();
        }

        public static void OnRichPresenceConnectionSuccess()
        {
            hp.UserName.Text = "@" + client.CurrentUser.Username;
        }

        public static string GetPfpUrl()
        {
            return PfpUrl;
        }

        public static string PfpUrl;
        public static bool bHasRpc;
    }
}
