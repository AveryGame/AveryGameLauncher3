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
            this.ShowsNavigationUI = false;
            var b = contentHost;
            var fade = new DoubleAnimation()
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.15),
            };
            Storyboard.SetTarget(fade, b);
            Storyboard.SetTargetProperty(fade, new PropertyPath(Button.OpacityProperty));
            var sb = new Storyboard();
            sb.Children.Add(fade);
            sb.Begin();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var b = contentHost;
            var fade = new DoubleAnimation()
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.15),
            };
            Storyboard.SetTarget(fade, b);
            Storyboard.SetTargetProperty(fade, new PropertyPath(Button.OpacityProperty));
            var sb = new Storyboard();
            sb.Children.Add(fade);
            sb.Begin();
            /*ThicknessAnimation contenthostMargin = new ThicknessAnimation();
            contenthostMargin.From = new Thickness(0, 0, 0, 0);
            contenthostMargin.To = new Thickness(900, 0, -900, 0);
            contenthostMargin.Duration = TimeSpan.FromSeconds(0.15);
            contenthostMargin.FillBehavior = FillBehavior.HoldEnd;
            var thicknessAnim = new Storyboard();
            Storyboard.SetTargetName(contenthostMargin, "contentHost");
            Storyboard.SetTargetProperty(contenthostMargin, new PropertyPath(Grid.MarginProperty));
            thicknessAnim.Children.Add(contenthostMargin);
            thicknessAnim.Begin(this);
            thicknessAnim.Children.Remove(contenthostMargin);*/
            ChangelogPage clp = new ChangelogPage();
            this.ShowsNavigationUI = false;
            MainWindow bootStrapper = new MainWindow();
            this.RemoveLogicalChild(this.wowz2);
            Console.WriteLine("Removed logical child - calling new page.");
            bootStrapper.RemoveLogicalChildOnWindowLoad();
            await Delay(150);
            this.NavigationService.Navigate(clp);
        }

        private async void windowChange_MouseEnter(object sender, MouseEventArgs e)
        {
            int width = 103;
            int height = 21;
            for (int i = 0; i > 500; i++)
            {
                windowChange.Width = width;
                windowChange.Height = height;
                width = width + 3;
                height = height + 1;
                await Delay(25);
            }
        }

        private void windowChange_MouseLeave(object sender, MouseEventArgs e)
        {

        }
    }
}
