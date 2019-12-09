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
        public EventManagerSingleton Events { get; set; }

        private Event _selectedEvent;
        public Event SelectedEvent
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
                ((RelayCommand)CreateEventCommand).RaiseCanExecuteChanged();
                ((RelayCommand)RemoveEventCommand).RaiseCanExecuteChanged();
                ((RelayCommand)UpdateEventCommand).RaiseCanExecuteChanged();
            }
        }

        public int SelectedIndex { get; set; }

        public ICommand CreateEventCommand { get; set; }
        public ICommand RemoveEventCommand { get; set; }
        public ICommand UpdateEventCommand { set; get; }
        

        public EditEventViewModel()
        {
            Events=EventManagerSingleton.Instance;
            CreateEventCommand = new RelayCommand(CreateEvent, EventIsSelected);
            RemoveEventCommand = new RelayCommand(RemoveEvent, SelectedIndexNotSet);
            UpdateEventCommand = new RelayCommand(UpdateEvent, SelectedIndexNotSet);
        }

        public bool EventIsSelected()
        {
            return SelectedEvent != null;
        }

        public bool SelectedIndexNotSet()
        {
            return SelectedIndex != -1;
        }

        public void CreateEvent()
        {
            Events.Create(SelectedEvent);
        }

        public void RemoveEvent()
        {
            Events.RemoveAt(SelectedIndex);
        }

        public void UpdateEvent()
        {
            Events.Update(SelectedIndex, SelectedEvent);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
