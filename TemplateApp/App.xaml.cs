using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using TemplateApp.MVVM.Model;

namespace TemplateApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MyPass")))
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MyPass"));
            else
            {
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MyPass");
                if(!File.Exists(path+"/Data.json"))
                {
                    List<LogData> list = new List<LogData>()
                    {
                        new LogData()
                        {
                            type = "Discord",
                            username = "discordo",
                            password = "rofllmaoLol1!",
                            description = "Discordo Account",
                            website = "https://discord.com/",
                            ImageUri = "https://pf.turbointerl9.repl.co/Images/DiscordIcon.png"
                        },
                        new LogData()
                        {
                            type = "Google",
                            username = "Googelos",
                            password = "rofllm",
                            description = "Googgelle",
                            website = "https://accounts.google.com/",
                            ImageUri = "https://pf.turbointerl9.repl.co/Images/GoogleIcon.png"
                        },
                        new LogData()
                        {
                            type = "PayPal",
                            username = "pay me bisch",
                            password = "123456password",
                            description = "Money",
                            website = "https://www.paypal.com/",
                            ImageUri = "https://pf.turbointerl9.repl.co/Images/PayPalIcon.png"
                        },
                        new LogData()
                        {
                            type = "Spotify",
                            username = "musikus",
                            password = "passwordoman",
                            description = "i listen to musik",
                            website = "https://www.spotify.com/",
                            ImageUri = "https://pf.turbointerl9.repl.co/Images/SpotifyIcon.png"
                        },
                        new LogData()
                        {
                            type = "Steam",
                            username = "gamingman123",
                            password = "ImSoSecure!",
                            description = "banane",
                            website = "https://store.steampowered.com/",
                            ImageUri = "https://pf.turbointerl9.repl.co/Images/SteamIcon.png"
                        },
                        new LogData()
                        {
                            type = "Riot Games",
                            username = "LolInter",
                            password = "ImSoSecure!",
                            description = "feed",
                            website = "https://riotgames.com/",
                            ImageUri = "https://pf.turbointerl9.repl.co/Images/RiotIcon.png"
                        }

                    };
                    string json = JsonSerializer.Serialize(list);
                    File.WriteAllText(path+"/Data.json", json);
                }
            }
        }
    }
}
