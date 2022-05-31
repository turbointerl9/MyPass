using System.Collections.Generic;
using TemplateApp.MVVM.Model;

namespace TemplateApp.Core.Data
{
    public class DataStore : IDataService
    {
        public IEnumerable<LogData> GetLogData()
        {
            return new List<LogData>()
            {
                new LogData()
                {
                    description = "My Paypal Account",
                    password = "opferkind32",
                    type = "PayPal",
                    username = "turbointerl9",
                    ImageUri = "https://pf.turbointerl9.repl.co/Images/PayPalIcon.png",
                    website = "https://www.paypal.com/"
                },
                new LogData()
                {
                    description = "sdlfksoldfk",
                    password = "abcdefghelp",
                    type = "Steam",
                    username = "gamer2123",
                    ImageUri = "https://pf.turbointerl9.repl.co/Images/SteamIcon.png",
                    website = "https://store.steampowered.com/?l=german"
                },
                new LogData()
                {
                    description = "Main League of Legends und Valorant Account, in Riot Client einloggen",
                    password = "AHAHHAAHHAHAHAHA",
                    type = "Riot Games",
                    username = "leaguenoob123",
                    ImageUri = "https://pf.turbointerl9.repl.co/Images/RiotIcon.png",
                    website = "https://www.riotgames.com/"
                },
                new LogData()
                {
                    description = "ggoooogele",
                    password = "lmaorofl93!",
                    type = "Google",
                    username = "googlemail@gmail.com",
                    ImageUri = "https://pf.turbointerl9.repl.co/Images/GoogleIcon.png",
                    website = "https://mail.google.com/"
                },
                new LogData()
                {
                    description = "Mein main Discord Account, warum man auch immer 2 haben sollte, Wrapped der Text?",
                    password = "rofllmaoLol12!",
                    type = "Discord",
                    username = "discordo",
                    ImageUri = "https://pf.turbointerl9.repl.co/Images/DiscordIcon.png",
                    website = "https://discord.com/"
                }
            };
        }
    }
}
