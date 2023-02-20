using BuburitoStore.Core;
using BuburitoStore.Models;
using BuburitoStore.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BuburitoStore.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public HomeViewModel HomeVM { get; private set; }
        public FrostShrineViewModel FrostVM { get; private set; }
        public KanyeRestViewModel KanyeVM { get; set; }
        public AsyncTestViewModel SpareVM { get; set; }

        //public KanyeRestViewModel KanyeVM { get; set; }

        public RelayCommand CloseAppCommand { get; set; }
        public RelayCommand MinimiseAppCommand { get; set; }
        public RelayCommand WindowStateCommand { get; set; }

        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand FrostShrineCommand { get; set; }

        public RelayCommand SpareViewCommand { get; set; }
        public RelayCommand KanyeViewCommand { get; set; }

        private readonly Window _window;

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

        public MainViewModel(MainWindow window)
        {
            //var homeWindow = new HomeView();
            HomeVM = new HomeViewModel();
            FrostVM = new FrostShrineViewModel();
            KanyeVM = new KanyeRestViewModel();
            SpareVM = new AsyncTestViewModel();
            //homeWindow.DataContext = HomeVM;

            CurrentView = HomeVM;

            _window = window;
            CloseAppCommand = new RelayCommand(e => _window.Close());

            MinimiseAppCommand = new RelayCommand(e => _window.WindowState = WindowState.Minimized);

            WindowStateCommand = new RelayCommand(e =>
            {
                if (_window.WindowState != WindowState.Maximized)
                    _window.WindowState = WindowState.Maximized;
                else
                    _window.WindowState = WindowState.Normal;

            });

            HomeViewCommand = new RelayCommand(e => CurrentView = HomeVM);
            FrostShrineCommand = new RelayCommand(e => CurrentView = FrostVM);
            SpareViewCommand = new RelayCommand(e => CurrentView = SpareVM);

            SpareViewCommand = new RelayCommand(e => CurrentView = KanyeVM);


            //ContentElement thing;
            
        }


    }
}
