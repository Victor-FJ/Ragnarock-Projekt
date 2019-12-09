using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using RagnarockApp.EventArsen.Model;
using System.Windows.Input;
using RagnarockApp.Common;
using RagnarockApp.Annotations;

namespace RagnarockApp.EventArsen.ViewModel
{
    public class EventViewModel : INotifyPropertyChanged
    {
        public EventManagerSingleton Events { get; set; }

        public EventViewModel()
        {
            Events = EventManagerSingleton.Instance;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
