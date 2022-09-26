using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using static System.Threading.Tasks.Task;
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
            Delay(150);
            Console.WriteLine("LAUNCHER STATUS ENUM IS " + Services.Enums.status);
            if (Services.Enums.status == Services.Enums.LauncherStatus.initializing)
            {
                InitializeRPC();
            }
            else if (Services.Enums.status == Services.Enums.LauncherStatus.rpcInitialized)
            {
                userImage.ImageSource = new BitmapImage(new Uri(Services.RichPresenceService.client.CurrentUser.GetAvatarURL(User.AvatarFormat.PNG, User.AvatarSize.x128)));
            }
        }

        private async void InitializeRPC()
        {
            loaderHost.Margin = new Thickness(0, 0, 0, 0);
            loaderHost.Opacity = 1;
            await Delay(2000);
            userImage.ImageSource = new BitmapImage(new Uri(Services.RichPresenceService.client.CurrentUser.GetAvatarURL(User.AvatarFormat.PNG, User.AvatarSize.x128)));
            Services.AnimationHandler.FadeOut(loaderHost);
            await Delay(150);
            loaderHost.Margin = new Thickness(69420, 69420, 69420, 69420);
            Services.Enums.status = Services.Enums.LauncherStatus.rpcInitialized;
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
    }
}
