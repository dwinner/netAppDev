using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Threading;
using System.Diagnostics;

namespace BallWorld.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        private PropertyChangedEventHandler propertyChangedEvent;

        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                propertyChangedEvent += value;
            }
            remove
            {
                propertyChangedEvent -= value;
            }
        }

        protected void FirePropertyChangedEvent(string propertyName)
        {
            if (propertyChangedEvent != null)
            {
                propertyChangedEvent(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
