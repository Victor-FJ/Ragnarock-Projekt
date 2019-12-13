using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using RagnarockApp.Annotations;
using RagnarockApp.Common;
using RagnarockApp.EventArsen.Model;
using RagnarockApp.Persistency;

namespace RagnarockApp.EventArsen.ViewModel
{
    public class CreateEventViewModel: INotifyPropertyChanged
    {
        private EventManagerSingleton Events;

        private Event _selectedEvent;

        public Event SelectedEvent
        {
            get { return _selectedEvent;}
            set
            {
                _selectedEvent = value;
                if (value == null)
                {
                    _selectedEvent=new Event();
                }
                OnPropertyChanged();
                ((RelayCommand)CreateEventCommand).RaiseCanExecuteChanged();
            }
        }

        public int SelectedIndex { get; set; }
        public ICommand CreateEventCommand { get; set; }

        public CreateEventViewModel()
        {
            Events=EventManagerSingleton.Instance;
            CreateEventCommand = new RelayCommand(CreateEvent);
            SelectedEvent = new Event();
        }

        public void CreateEvent()
        {
            Events.Create(SelectedEvent);
            Save();
        }

        private async void Save()
        {
            await PersistencyFacade.SaveEventsAsJsonAsync(EventManagerSingleton.Instance.Events);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
