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
            Console.WriteLine("Loaded all page components and finished animation.");
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
