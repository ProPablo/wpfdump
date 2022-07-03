using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuburitoStore.Models
{
    public class Game
    {
        public Guid Id = Guid.NewGuid();
        public string Name { get; set; }
        public float Price { get; set; }
        public string URL { get; set; }
        public string ImageURL { get; init; }

        public string SmallTitle { get; set; }

        public bool isSteam { get; set; }

        public RelayCommand LaunchGameCommand { get; set; }

        public Game(string smallTitle = default)
        {
            isSteam = false;
            LaunchGameCommand = new RelayCommand(LaunchGame);
            SmallTitle = smallTitle;
            //if (SmallTitle == null) SmallTitle = Name.Trim(); //Object initializer syntax works as syntactic sugar by filling in props after ctor
            ImageURL = $"/BuburitoStore;component/Assets/{SmallTitle}.png";
        }

        private void LaunchGame(object _)
        {
            Process gameProc = new Process();
            gameProc.StartInfo = new ProcessStartInfo(URL)
            {
                UseShellExecute = true,
                Verb = "open"
            };
            gameProc.Start();
        }

        //https://docs.microsoft.com/en-us/dotnet/desktop/wpf/graphics-multimedia/painting-with-solid-colors-and-gradients-overview?view=netframeworkdesktop-4.8
    }
}
