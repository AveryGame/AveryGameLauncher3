using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using static System.Threading.Tasks.Task;
using static AgsLauncherV3.Services.Enums;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using XamlAnimatedGif;
using DiscordRPC;
using DiscordRPC.Message;
using DiscordRPC.Logging;
using System.Threading;
using Newtonsoft.Json;
using System.IO;

namespace AgsLauncherV3
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
            Services.AnimationHandler.FadeIn(contentHost);
            languageSetting = LocalizedLanguage.english;
            Delay(150);
            Console.WriteLine("LAUNCHER STATUS ENUM IS " + Services.Enums.status);
            if (status == LauncherStatus.initializing)
            {
                InitializeRPC();
            }
            else if (status == LauncherStatus.rpcInitialized)
            {
                userImage.ImageSource = new BitmapImage(new Uri(Services.RichPresenceService.client.CurrentUser.GetAvatarURL(User.AvatarFormat.PNG, User.AvatarSize.x128)));
            }
        }

        private async void InitializeRPC()
        {
            loaderHost.Margin = new Thickness(0, 0, 0, 0);
            loaderHost.Opacity = 1;
            Append(languageSetting);
            await Delay(2000);
            if (Services.RichPresenceService.client.CurrentUser == null)
            {
                AppendRPCWelcome("Null", languageSetting, false);
            }
            else if (Services.RichPresenceService.client.CurrentUser != null)
            {
                AppendRPCWelcome(Services.RichPresenceService.client.CurrentUser.Username, languageSetting, true);
                userImage.ImageSource = new BitmapImage(new Uri(Services.RichPresenceService.client.CurrentUser.GetAvatarURL(User.AvatarFormat.PNG, User.AvatarSize.x128)));
            }
            Services.AnimationHandler.FadeOut(loaderHost);
            Services.AnimationHandler.MovementAnimation(loaderHost, 0.15, new Thickness(0, 0, 0, 0), new Thickness(0, 230, 0, 0));
            await Delay(150);
            loaderHost.Margin = new Thickness(69420, 69420, 69420, 69420);
            status = LauncherStatus.rpcInitialized;
        }

        private async void Button_Click(object sender, MouseButtonEventArgs e)
        {
            Services.AnimationHandler.FadeOut(contentHost);
            await Delay(150);
            ChangelogPage clp = new ChangelogPage();
            this.NavigationService.Navigate(clp);
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            Services.AnimationHandler.FadeIn(libraryHighlight);
        }

        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            Services.AnimationHandler.FadeOut(libraryHighlight);
        }

        private void Append(LocalizedLanguage language)
        {
            string languageData = File.ReadAllText("root/lang/" + language + ".json");
            jsonFields lang = JsonConvert.DeserializeObject<jsonFields>(languageData);
            MessageBox.Show(lang.libraryButton);
            libraryButton.Text = lang.libraryButton;
        }
        private void AppendRPCWelcome(string rpcName, LocalizedLanguage language, bool hasRpc)
        {
            if (hasRpc)
            {
                string languageData = File.ReadAllText("root/lang/" + language + ".json");
                jsonFields lang = JsonConvert.DeserializeObject<jsonFields>(languageData);
                MessageBox.Show(lang.rpcWelcome + rpcName + "!");
                userWelcome.Text = lang.rpcWelcome + rpcName + "!";
            }
            else if (!hasRpc)
            {
                string languageData = File.ReadAllText("root/lang/" + language + ".json");
                jsonFields lang = JsonConvert.DeserializeObject<jsonFields>(languageData);
                MessageBox.Show(lang.welcomeDefault);
                userWelcome.Text = lang.welcomeDefault;
            }
        }

        internal class jsonFields
        {
            public string libraryButton { get; set; }
            public string rpcWelcome { get; set; }
            public string welcomeDefault { get; set; }
        }
    }
}
