using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChangelogPage clp = new ChangelogPage();
            this.ShowsNavigationUI = false;
            MainWindow bootStrapper = new MainWindow();
            this.RemoveLogicalChild(this.wowz2);
            bootStrapper.RemoveLogicalChildOnWindowLoad();
            this.NavigationService.Navigate(clp);
            
        }
    }
}
