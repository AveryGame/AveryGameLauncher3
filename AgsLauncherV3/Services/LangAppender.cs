using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static AveryGameLauncher3.Services.Enums;

namespace AveryGameLauncher3.Services
{
    internal class jsonFields
    {
        public string libraryButton { get; set; }
        public string rpcWelcome { get; set; }
    }
    internal class LangAppender
    {
        public static void Append(LocalizedLanguage language)
        {
            HomePage hp = new HomePage();
            string languageData = File.ReadAllText("root/lang/" + language + ".json");
            jsonFields lang = JsonConvert.DeserializeObject<jsonFields>(languageData);
        }
        public static void AppendRPCWelcome(string rpcName, LocalizedLanguage language)
        {
            HomePage hp = new HomePage();
            string languageData = File.ReadAllText("root/lang/" + language + ".json");
            jsonFields lang = JsonConvert.DeserializeObject<jsonFields>(languageData);
            hp.userWelcome.Text = lang.rpcWelcome + rpcName + "!";
        }
    }
}
