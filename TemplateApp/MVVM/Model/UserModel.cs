using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateApp.Core;

namespace TemplateApp.MVVM.Model
{
    public class UserModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public ObservableCollection<LogData> Data { get; set; }
    }

    public class LogData
    {
        public string type { get; set; }
        public string username { get; set; }

        public string password { get; set; }
        public string description { get; set; }
        public string website { get; set; }
        public string ImageUri { get; set; }
    }
}
