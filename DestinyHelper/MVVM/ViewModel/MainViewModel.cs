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

        public HomeViewModel HomeVm { get; set; }
        public OpenWorldViewModel OpenWorldVm { get; set; }

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

            CurrentView = HomeVm;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVm;
            });

            OpenWorldViewCommand = new RelayCommand(o =>
            {
                CurrentView = OpenWorldVm;
            });
        }
    }
}
