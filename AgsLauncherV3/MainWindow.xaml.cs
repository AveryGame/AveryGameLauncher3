global using static AveryGameLauncher3.Services.PublicVariables;
using System;
using System.Windows;
using System.Runtime.InteropServices;
using System.Reflection;
using System.IO;
using System.Windows.Interop;
using Newtonsoft.Json.Linq;

namespace AveryGameLauncher3
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

        string PATH = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGameLauncher3\\";
        
        public MainWindow()
        {
            AssignVars();
            IntPtr hWnd = new WindowInteropHelper(this).EnsureHandle();
            var attribute = DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
            var preference = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND;
            DwmSetWindowAttribute(hWnd, attribute, ref preference, sizeof(uint));
            InitializeComponent();
            if (!string.IsNullOrWhiteSpace(Environment.GetCommandLineArgs()[1]) && Environment.GetCommandLineArgs()[1] == "--devrunnerstate.1")
            {
                AllocConsole();
                RunDeveloperBuildEvents();
                Console.WriteLine("AveryGameLauncher3.App//Hana..Bootstrap() -- Allocated console at " + DateTime.Now.ToString());
            }
            Services.Enums.status = Services.Enums.LauncherStatus.initializing;
            Services.RichPresenceService.Handler();
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
        public void AssignVars()
        {
            @pp = new Pages.PlayPage();
            @hp = new HomePage();
            @clp = new Pages.ChangelogPage();
            @mw = this;
        }
        public void RemoveLogicalChildOnWindowLoad()
        {
            this.RemoveLogicalChild(hp);
            this.RemoveLogicalChild(clp);
        }
        public void RunDeveloperBuildEvents()
        {
            JObject rss = JObject.Parse(File.ReadAllText("../../../Build.agdev"));
            int IncrementedBuildNumber = int.Parse(rss["BuildNumber"].ToString());
            IncrementedBuildNumber++;
            rss["BuildNumber"] = IncrementedBuildNumber;
            File.WriteAllText("../../../Build.agdev", rss.ToString());
            hp.DevInfLabel.Content = $"{rss["Version"].ToString()} Build {IncrementedBuildNumber} | Flags: {rss["VersionFlags"].ToString()}";

        }

        // The enum flag for DwmSetWindowAttribute's second parameter, which tells the function what attribute to set.
        // Copied from dwmapi.h
        public enum DWMWINDOWATTRIBUTE
        {
            DWMWA_WINDOW_CORNER_PREFERENCE = 33
        }

        // The DWM_WINDOW_CORNER_PREFERENCE enum for DwmSetWindowAttribute's third parameter, which tells the function
        // what value of the enum to set.
        // Copied from dwmapi.h
        public enum DWM_WINDOW_CORNER_PREFERENCE
        {
            DWMWCP_DEFAULT = 0,
            DWMWCP_DONOTROUND = 1,
            DWMWCP_ROUND = 2,
            DWMWCP_ROUNDSMALL = 3
        }

        // Import dwmapi.dll and define DwmSetWindowAttribute in C# corresponding to the native function.
        [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        internal static extern void DwmSetWindowAttribute(IntPtr hwnd,
                                                         DWMWINDOWATTRIBUTE attribute,
                                                         ref DWM_WINDOW_CORNER_PREFERENCE pvAttribute,
                                                         uint cbAttribute);
    }
}
