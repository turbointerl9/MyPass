using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TemplateApp.Core;

namespace TemplateApp.MVVM.View
{
    /// <summary>
    /// Interaktionslogik für SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Autostart.Add();
            File.WriteAllText("Config.cfg", "1");
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Autostart.Remove();
            File.WriteAllText("Config.cfg", "0");
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            string mode = File.ReadAllText("Config.cfg");

            if (mode == "1")
                StartUpBox.IsChecked = true;
            else
                StartUpBox.IsChecked = false;
        }
    }
}
