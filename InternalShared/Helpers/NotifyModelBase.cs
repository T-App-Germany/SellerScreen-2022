using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InternalShared
{
    /// <summary>
    /// Base class to support property change notifications
    /// </summary>
    public abstract class NotifyModelBase : INotifyPropertyChanged
    {
        public NotifyModelBase()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected void NotifyChangedThis([CallerMemberName] string propertyName = null)
        {
            if (propertyName != null)
                NotifyPropertyChanged(propertyName);
        }
    }
}
