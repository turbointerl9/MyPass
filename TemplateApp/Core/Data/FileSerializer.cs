using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TemplateApp.Core.Data
{
    public static class FileSerializer
    {
        static string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MyPass");
        static string json = "";

        public static void UpdateFile<T>(ObservableCollection<T> list)
        {
            json = JsonSerializer.Serialize(list);
            File.WriteAllText(path + "/Data.json", json);
        }
        public static ObservableCollection<T> LoadFile<T>()
        {
            json = File.ReadAllText(path + "/Data.json");
            return JsonSerializer.Deserialize<ObservableCollection<T>>(json);
        }
    }
}
