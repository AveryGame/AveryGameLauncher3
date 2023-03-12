using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace AveryGameLauncher3.Services
{
    internal class WindowsUtilities
    {
        [DllImport("shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);
        public static void SetAssociation(string extension, string keyName, string openWith, string fileDescription)
        {
            RegistryKey subKey1 = ((RegistryKey)Registry.ClassesRoot).CreateSubKey(extension);
            if (subKey1 != null)
                subKey1.SetValue("", (object)keyName);
            RegistryKey subKey2 = ((RegistryKey)Registry.ClassesRoot).CreateSubKey(keyName);
            if (subKey2 != null)
                subKey2.SetValue("", (object)fileDescription);
            if (subKey2 != null)
            {
                RegistryKey subKey3 = subKey2.CreateSubKey("DefaultIcon");
                if (subKey3 != null)
                {
                    string str1 = "";
                    string str2 = openWith.Replace("AgsLauncherV3.exe", "AgsLauncherV3.exe") + "\\Data\\AGLPDisplayEngine\\AGLVF.ico";
                    subKey3.SetValue(str1, (object)str2);
                }
            }
            RegistryKey registryKey1 = subKey2 != null ? subKey2.CreateSubKey("Shell") : (RegistryKey)null;
            if (registryKey1 != null)
            {
                string str1 = "open";
                RegistryKey subKey3 = registryKey1.CreateSubKey(str1);
                if (subKey3 != null)
                {
                    string str2 = "command";
                    RegistryKey subKey4 = subKey3.CreateSubKey(str2);
                    if (subKey4 != null)
                    {
                        string str3 = "";
                        string str4 = "\"" + openWith + "\" \"%1\"";
                        subKey4.SetValue(str3, (object)str4);
                    }
                }
            }
            if (subKey1 != null)
                subKey1.Close();
            if (subKey2 != null)
                subKey2.Close();
            if (registryKey1 != null)
                registryKey1.Close();
            RegistryKey registryKey2 = ((RegistryKey)Registry.CurrentUser).OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FileExts\\" + extension, true);
            if (registryKey2 != null)
            {
                string str = "UserChoice";
                int num = 0;
                registryKey2.DeleteSubKey(str, num != 0);
            }
            if (registryKey2 != null)
                registryKey2.Close();
            WindowsUtilities.SHChangeNotify(134217728U, 0U, IntPtr.Zero, IntPtr.Zero);
        }
    }
}
