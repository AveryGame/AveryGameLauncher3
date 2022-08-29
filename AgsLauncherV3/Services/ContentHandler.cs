using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace AgsLauncherV3.Services
{
    internal class ContentHandler
    {
        public static void HandlePageChange(object destinationPage)
        {
            MainWindow bootStrapperHost = new MainWindow();
            bootStrapperHost.RemoveLogicalChildOnWindowLoad();
            bootStrapperHost.Content = destinationPage;
        }
    }
}
