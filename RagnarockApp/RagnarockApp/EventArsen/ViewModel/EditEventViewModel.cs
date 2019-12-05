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
    public class EditEventViewModel:INotifyPropertyChanged
    {
        private EventManagerSingleton eventManagerSingleton;


        private Event _selectedEvent;

        public Event SelcetedEvent
        {
            get { return _selectedEvent; }
            set
            {
                _selectedEvent = value;
                if(value == null)
                {
                    _selectedEvent = new Event();
                }
                OnPropertyChanged();
            }
        }

        private ICommand _addcommand;

       

        public ICommand AddCommand
        {
            get { return _addcommand; }
            set { _addcommand = value; }
        }

        public EditEventViewModel()
        {
            eventManagerSingleton = EventManagerSingleton.Instance;
            _addcommand = new RelayCommand(Add, EventIsSelected);
            SelcetedEvent = new Event();
        }

        public bool EventIsSelected()
        {
            return (true);
        }

        public void Add()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
