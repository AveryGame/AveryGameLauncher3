using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using static System.Threading.Tasks.Task;
using static AveryGameLauncher3.Services.Enums;
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
using AveryGameLauncher3.Services;
using System.Threading.Tasks;
using System.Windows.Media.Effects;
using Wpf.Ui.Controls;

namespace AveryGameLauncher3
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : UiPage
    {
        public HomePage()
        {
            InitializeComponent();
            Services.AnimationHandler.FadeIn(contentHost, 0.5);
            languageSetting = LocalizedLanguage.english;
            Delay(150);
            Console.WriteLine("LAUNCHER STATUS ENUM IS " + Services.Enums.status);
            wowz2.Navigate(pp);
            switch (status)
            {
                case LauncherStatus.initializing:
                    InitializeRPC();
                    break;
                case LauncherStatus.rpcInitialized:
                    userImage.ImageSource = new BitmapImage(new Uri(Services.RichPresenceService.client.CurrentUser.GetAvatarURL(User.AvatarFormat.PNG, User.AvatarSize.x128)));
                    break;
            }
        }

        private async void InitializeRPC()
        {
            //Append(languageSetting);
            await Delay(2000);
            if (Services.RichPresenceService.client.CurrentUser == null)
            {
                //AppendRPCWelcome("Null", languageSetting, false);
            }
            else if (Services.RichPresenceService.client.CurrentUser != null)
            {
                //AppendRPCWelcome(Services.RichPresenceService.client.CurrentUser.Username, languageSetting, true);
                userImage.ImageSource = new BitmapImage(new Uri(Services.RichPresenceService.client.CurrentUser.GetAvatarURL(User.AvatarFormat.PNG, User.AvatarSize.x128)));
            }
            await Delay(150);
            status = LauncherStatus.rpcInitialized;
        }

        private async void Button_Click(object sender, MouseButtonEventArgs e)
        {
            Services.AnimationHandler.FadeOut(contentHost, 0.15);
            await Delay(150);
            this.NavigationService.Navigate(clp);
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
        }

        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
        }

        private void Append(LocalizedLanguage language)
        {
            string languageData = File.ReadAllText("root/lang/" + language + ".json");
            jsonFields lang = JsonConvert.DeserializeObject<jsonFields>(languageData);
        }
        private void AppendRPCWelcome(string rpcName, LocalizedLanguage language, bool hasRpc)
        {
            if (Services.RichPresenceService.bHasRpc)
            {
                string languageData = File.ReadAllText("root/lang/" + language + ".json");
                jsonFields lang = JsonConvert.DeserializeObject<jsonFields>(languageData);
                userWelcome.Text = lang.rpcWelcome + rpcName + "!";
            }
            else if (!Services.RichPresenceService.bHasRpc)
            {
                string languageData = File.ReadAllText("root/lang/" + language + ".json");
                jsonFields lang = JsonConvert.DeserializeObject<jsonFields>(languageData);
                userWelcome.Text = lang.welcomeDefault;
            }
        }

        internal class jsonFields
        {
            public string libraryButton { get; set; }
            public string rpcWelcome { get; set; }
            public string welcomeDefault { get; set; }
        }

        /*
         * <!--Library: 0,102,0,0-->
            <!--Bugs: 0,137,0,0-->
            <!--Changelog: 0,170,0,0-->
            <!--News: 0,205,0,0-->
            <!--Settings: 0,239,0,0--> */

        private void BugsButton_MouseEnter(object sender, MouseEventArgs e)
        {
            AnimationHandler.FadeIn(BugsButtonDropShadow, 0.3);
        }

        private void BugsButton_MouseLeave(object sender, MouseEventArgs e)
        {
            AnimationHandler.FadeOut(BugsButtonDropShadow, 0.3);
        }

        /*private void ChangelogButton_MouseEnter(object sender, MouseEventArgs e)
        {
            AnimatePageIndicatorToChangelog();

            AnimationHandler.FadeIn(ChangelogDropShadowOutlined, 0.5);
        }

        private void ChangelogButton_MouseLeave(object sender, MouseEventArgs e)
        {
            AnimationHandler.FadeOut(ChangelogDropShadowOutlined, 0.5);
        }

        private void NewsButton_MouseEnter(object sender, MouseEventArgs e)
        {
            AnimatePageIndicatorToNews();

            AnimationHandler.FadeIn(NewsDropShadowOutlined, 0.5);
        }

        private void NewsButton_MouseLeave(object sender, MouseEventArgs e)
        {
            AnimationHandler.FadeOut(NewsDropShadowOutlined, 0.5);
        }

        private void SettingsButton_MouseEnter(object sender, MouseEventArgs e)
        {
            AnimatePageIndicatorToSettings();
            
            AnimationHandler.FadeIn(SettingsDropShadowOutlined, 0.5);
        }

        private void SettingsButton_MouseLeave(object sender, MouseEventArgs e)
        {
            AnimationHandler.FadeOut(SettingsDropShadowOutlined, 0.5);
        }*/
        
        private async void AnimatePageIndicator(Thickness position)
        {
            //i dont know what the hell i did that causes it to do the animation it did but i like it so im keeping it
            AnimationHandler.FadeOut(PageIndicator, 0.15);
            await Task.Delay(150);
            PageIndicator.Margin = position;
            AnimationHandler.FadeIn(PageIndicator, 0.15);
        }

        private void BugsButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AnimatePageIndicator(new Thickness(0, 137, 0, 0));
            wowz2.Opacity = 0;
            wowz2.Visibility = Visibility.Visible;
            wowz2.Navigate(clp);
            AnimationHandler.FadeIn(wowz2, 0.2);
        }

        private void TrafficLightGotFocus(object sender, MouseEventArgs e)
        {
            TrafficLightClose.Visibility = Visibility.Visible;
            TrafficLightMinimize.Visibility = Visibility.Visible;
        }

        private void TrafficLightLostFocus(object sender, MouseEventArgs e)
        {
            TrafficLightClose.Visibility = Visibility.Hidden;
            TrafficLightMinimize.Visibility = Visibility.Hidden;
        }

        private void AppClose(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown(0);
        }

        private void AppMinimize(object sender, MouseButtonEventArgs e)
        {
            mw.WindowState = WindowState.Minimized;
        }

        private void DragBarHandle(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton==MouseButtonState.Pressed){mw.DragMove();}
        }

        private void PlayButton_MouseEnter(object sender, MouseEventArgs e)
        {
            AnimationHandler.FadeIn(PlayButtonDropShadow, 0.3);
        }

        private void PlayButton_MouseLeave(object sender, MouseEventArgs e)
        {
            AnimationHandler.FadeOut(PlayButtonDropShadow, 0.3);
        }

        private void PlayButton_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
