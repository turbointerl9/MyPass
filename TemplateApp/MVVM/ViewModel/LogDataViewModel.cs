using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TemplateApp.Core;
using TemplateApp.Core.Data;
using TemplateApp.Core.Data.Security;
using TemplateApp.MVVM.Model;

namespace TemplateApp.MVVM.ViewModel
{
    public class LogDataViewModel : MainViewModel
    {
      
        private LogData logData;

        public LogData NewItem
        {
            get { return logData; }
            set 
            { 
                logData = value;
                OnPropertyChanged();
            }
        }


        public RelayCommand SaveChangesCommand { get; set; }
        public LogDataViewModel()
        {
            SaveChangesCommand = new RelayCommand(o =>
            {
                if (!Username.IsEmpty() && !Password.IsEmpty() && !Description.IsEmpty() && !Type.IsEmpty() && !Website.IsEmpty())
                {
                    NewItem = new LogData()
                    {
                        username = Username,
                        password = Password,
                        type = Type,
                        website = Website,
                        description = Description,
                        ImageUri = $"https://www.google.com/s2/favicons?sz=64&domain_url={Website}"
                    };
                    LogDatas.Add(NewItem);
                    FileSerializer.UpdateFile(LogDatas);
                }
                else
                    MessageBox.Show("Eingabe war leer", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            });
        }
    }

    public static class extensions
    {
        public static bool IsEmpty(this string s)
        {
            return String.IsNullOrEmpty(s);
        }
    }
}
