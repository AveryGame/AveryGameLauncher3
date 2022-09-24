using System;
using System.Windows;
using System.Runtime.InteropServices;
using System.Reflection;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Principal;
using System.IO;

namespace AgsLauncherV3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("Kernel32")]
        public static extern void AllocConsole();

        [DllImport("Kernel32", SetLastError = true)]
        public static extern void FreeConsole();

        string PATH = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame\\AveryGameLauncher3";
        public MainWindow()
        {
            InitializeComponent();
            if (Environment.GetCommandLineArgs()[1] == "--devrunnerstate.1")
            {
                AllocConsole();
                Console.WriteLine("AveryGameLauncher3.App//Hana..Bootstrap() -- Allocated console at " + DateTime.Now.ToString());
            }
            HomePage hp = new HomePage();
            this.Height = 450;
            this.Width = 800;
            Console.WriteLine("[LOGBOOSTRAP]: Setting BootStrapperHost content to HomePage content.");
            bootStrapperHost.Content = hp;
            if (!Directory.Exists(PATH))
            {
                Directory.CreateDirectory(PATH);
                File.WriteAllText(PATH + "\\PATH", Assembly.GetExecutingAssembly().Location.Replace(".dll", ".exe").ToString());
            }
        }

        private static bool RunElevated(string fileName)
        {
            //MessageBox.Show("Run: " + fileName);
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.UseShellExecute = true;
            processInfo.Verb = "runas";
            processInfo.FileName = fileName;
            try
            {
                MessageBox.Show("The AveryGame launcher needs to acquire administrative rights to finish the first-time setup.");
                Process.Start(processInfo);
                return true;
            }
            catch (Win32Exception)
            {
                //Do nothing. Probably the user canceled the UAC window
            }
            return false;
        }
        public void RemoveLogicalChildOnWindowLoad()
        {
            HomePage hp = new HomePage();
            this.RemoveLogicalChild(hp);
            ChangelogPage clp = new ChangelogPage();
            this.RemoveLogicalChild(clp);
        }
    }
}
