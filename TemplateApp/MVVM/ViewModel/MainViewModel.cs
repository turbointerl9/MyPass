using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using TemplateApp.Core;
using TemplateApp.Core.Data;
using TemplateApp.Core.Data.Security;
using TemplateApp.MVVM.Model;
using TemplateApp.MVVM.View;

namespace TemplateApp.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        public HomeViewModel HomeVM { get; set; }
        public SettingsViewModel SettingsVM { get; set; }
        public EditViewModel EditVM { get; set; }
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand SettingsViewCommand { get; set; }
        public RelayCommand EditViewCommand { get; set; }
        public RelayCommand CancelViewCommand { get; set; }

        public HomeView HView;

        private object _editMode;

        public object EditMode
        {
            get { return _editMode; }
            set
            {
                _editMode = value;
                OnPropertyChanged();
            }
        }


        private string _searchInput;

        public string SearchInput
        {
            get { return _searchInput; }
            set
            {
                _searchInput = value;
                OnPropertyChanged();
                if (SearchInput != "" && SearchInput != null)
                    LogDatas = Convert(LogDatas.Where(x => x.username.ToLower().Contains(SearchInput.ToLower()) || x.type.ToLower().Contains(SearchInput.ToLower())));
                else
                    PopulateCollection();
            }
        }

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        private string _currentUser;

        public string CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value;
                OnPropertyChanged();
            }
        }

        /*private ObservableCollection<LogData> logDatas;

        public ObservableCollection<LogData> LogDatas
        {
            get { return logDatas; }
            set
            {
                SetProperty(ref logDatas, value);
            }
        }*/

        public ObservableCollection<LogData> LogDatas { get; set; }

        private LogData _currentItem;

        public LogData CurrentItem
        {
            get { return _currentItem; }
            set
            {
                _currentItem = value;
                OnPropertyChanged();
                if (value != null)
                {
                    Strength = PasswordAdvisor.CheckStrength(value.password);
                    EditMode = null;
                }
            }
        }

        private double _strength;

        public double Strength
        {
            get { return _strength; }
            set
            {
                _strength = value;
                OnPropertyChanged();
            }
        }

        private string _username;

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
                if (!value.IsEmpty())
                    Strength = PasswordAdvisor.CheckStrength(value);
            }
        }

        private string _type;

        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged();
            }
        }

        private string _website;

        public string Website
        {
            get { return _website; }
            set
            {
                _website = value;
                OnPropertyChanged();
            }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        private LogData _editableItem;

        public LogData EditableItem
        {
            get { return _editableItem; }
            set
            {
                _editableItem = value;
                OnPropertyChanged();
                if (!value.username.IsEmpty() && !value.password.IsEmpty() && !value.description.IsEmpty() 
                    && !value.website.IsEmpty()) 
                    LogDatas.Add(value);
            }
        }


        public RelayCommand SearchCommand { get; set; }

        public RelayCommand CopyPasswordCommand { get; set; }
        public RelayCommand CopyNameCommand { get; set; }
        public RelayCommand LogoutCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand AddCommand { get; set; }


        public MainViewModel()
        {
            LogDatas = new ObservableCollection<LogData>();
            HomeVM = new HomeViewModel();
            SettingsVM = new SettingsViewModel();
            EditVM = new EditViewModel();
            CurrentView = HomeVM;
            CurrentUser = "Arthur";
            EditMode = null;

            AddCommand = new RelayCommand(o =>
            {
                LogDatas.Add(new LogData()
                {
                    username = "discorduser1",
                    password = "sample1",
                    description ="sampleaccount",
                    type = "discord",
                    website = "discord.com",
                    ImageUri = "https://www.google.com/s2/favicons?sz=64&domain_url=discord.com"
                });
            });

            EditViewCommand = new RelayCommand(o =>
            {
                EditMode = EditVM;
            });

            DeleteCommand = new RelayCommand(o =>
            {
                LogDatas.Remove(CurrentItem);
                FileSerializer.UpdateFile(LogDatas);
            });

            LogoutCommand = new RelayCommand(o =>
            {
                Application.Current.Shutdown();
            });

            CopyPasswordCommand = new RelayCommand(o =>
            {
                Clipboard.SetText(CurrentItem.password);
            });
            CopyNameCommand = new RelayCommand(o =>
            {
                Clipboard.SetText(CurrentItem.username);
            });

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            SettingsViewCommand = new RelayCommand(o =>
            {
                CurrentView = SettingsVM;
            });

            SearchCommand = new RelayCommand(o =>
            {
                PopulateCollection();
                if (SearchInput != "" && SearchInput != null)
                    LogDatas = Convert(LogDatas.Where(x => x.username.ToLower().Contains(SearchInput.ToLower()) || x.type.ToLower().Contains(SearchInput.ToLower())));
            });

            CancelViewCommand = new RelayCommand(o =>
            {
                EditMode = null;
            });
            PopulateCollection();

        }
        public void PopulateCollection()
        {
            LogDatas.Clear();
            LogDatas = Convert(FileSerializer.LoadFile<LogData>().OrderBy(x => x.type));
        }

        ObservableCollection<T> Convert<T>(IEnumerable<T> original)
        {
            return new ObservableCollection<T>(original);
        }
    }
}
