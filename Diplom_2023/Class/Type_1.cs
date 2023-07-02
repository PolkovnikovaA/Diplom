using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Diplom_2023
{
    internal class Type_1 : INotifyPropertyChanged
    {
        private string nazv;
        public string Nazv
        {
            get { return nazv; }
            set
            {
                nazv = value;
                OnPropertyChanged("Nazv");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
