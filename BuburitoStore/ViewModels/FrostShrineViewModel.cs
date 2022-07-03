using BuburitoStore.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BuburitoStore.ViewModels
{
    public class FrostShrineViewModel
    {
        //public UserControl windowControl;
        public RelayCommand AughRelayCommand { get; set; }

        private List<SoundPlayer> aughs = new List<SoundPlayer>();

        public FrostShrineViewModel()
        {
            AughRelayCommand = new RelayCommand(Augh);
            var aughStrings = new string[] { "augh0", "augh1", "augh2" };
            //loaded as a resource, these are static (Embedded reosurces), if loaded as content it goes in a project folder and can be dynamic
            aughs = aughStrings.Select(s =>
            {
                Uri file = new Uri($"pack://application:,,,/BuburitoStore;component/Assets/{s}.wav");
                var soundStream = Application.GetResourceStream(file);
                var sound = new SoundPlayer(soundStream.Stream);
                return sound;
            }).ToList();
        }

        private void Augh(object input)
        {
            var aughIndex = Random.Shared.Next(aughs.Count);
            aughs[aughIndex].Play();
            Debug.WriteLine("Augh");
        }

    }
}
