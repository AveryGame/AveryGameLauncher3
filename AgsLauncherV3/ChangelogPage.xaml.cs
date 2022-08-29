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
    /// Interaction logic for ChangelogPage.xaml
    /// </summary>
    public partial class ChangelogPage : Page
    {
        public ChangelogPage()
        {
            HomePage mw = new HomePage();
            InitializeComponent();
            this.ShowsNavigationUI = false;
            mw.Content = new UserControl();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HomePage hp = new HomePage();
            this.ShowsNavigationUI = false;
            MainWindow bootStrapper = new MainWindow();
            bootStrapper.RemoveLogicalChildOnWindowLoad();
            this.NavigationService.Navigate(hp);
            
        }
    }
}
