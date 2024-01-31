using DestinyHelper.Core;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyHelper.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {

        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand OpenWorldViewCommand { get; set; }
        public RelayCommand MacroViewCommand { get; set; }


        public HomeViewModel HomeVm { get; set; }
        public OpenWorldViewModel OpenWorldVm { get; set; }
        public MacroViewModel MacroVm { get; set; }

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


        public MainViewModel() 
        {
            HomeVm = new HomeViewModel();
            OpenWorldVm = new OpenWorldViewModel();
            MacroVm = new MacroViewModel();

            CurrentView = HomeVm;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVm;
            });

            OpenWorldViewCommand = new RelayCommand(o =>
            {
                CurrentView = OpenWorldVm;
            });

            MacroViewCommand = new RelayCommand(o =>
            {
                CurrentView = MacroVm;
            });
        }
    }
}
