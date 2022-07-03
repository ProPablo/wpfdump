using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BuburitoStore.Core
{
    //This exists so that when we update through code, it is reflected on UI
    public class ObservableObject : INotifyPropertyChanged
    {
        //Use this to only change OnProperty changed values when this is active
        //Dont uneccesarily trigger UI update
        protected bool _isViewActive = false;
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
