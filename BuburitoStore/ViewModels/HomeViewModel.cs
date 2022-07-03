using BuburitoStore.Core;
using BuburitoStore.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace BuburitoStore.ViewModels
{
    public class HomeViewModel : ObservableObject
    {

        public ObservableCollection<Game> Games { get; set; }

        //private Game _selectedGame;
        //public Game SelectedGame
        //{
        //    get { return _selectedGame; }
        //    set
        //    {
        //        _selectedGame = value;
        //        OnPropertyChanged();
        //    }
        //}

        public Game SelectedGame { get; set; }


        private string timerText;

        public string TimerText
        {
            get { return timerText; }
            set
            {
                timerText = value;
                OnPropertyChanged("TimerText");
            }
        }

        public string CurrentGameText
        {
            get { return Games[SelectedIndex].Name; }
        }


        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                OnPropertyChanged();
            }
        }

        public DateTime startTime = DateTime.Now;


        public HomeViewModel()
        {
            SelectedIndex = 0;
            Games = new ObservableCollection<Game>(Db.games);
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Start();
            TimerText = DateTime.Now.ToString();

        }

        private void timer_Tick(object? sender, EventArgs e)
        {
            string dateTime = DateTime.Now.ToLongTimeString();
            double elapsedMillisecs = ((TimeSpan)(DateTime.Now - startTime)).TotalMilliseconds;
            TimerText = elapsedMillisecs.ToString();
            //Debug.WriteLine("Home vm timer");
            SelectedIndex = (SelectedIndex + 1) % Games.Count;
            //CommandManager.InvalidateRequerySuggested();
        }

    }
}
