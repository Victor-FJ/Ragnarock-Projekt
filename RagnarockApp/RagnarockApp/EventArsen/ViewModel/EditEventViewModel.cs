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
using RagnarockApp.EventArsen.View;
using RagnarockApp.Persistency;

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
                ((RelayCommand)GotoEventCommand).RaiseCanExecuteChanged();
                ((RelayCommand)RemoveEventCommand).RaiseCanExecuteChanged();
                ((RelayCommand)UpdateEventCommand).RaiseCanExecuteChanged();
            }
        }

        public int SelectedIndex { get; set; }

        public ICommand GotoEventCommand { get; set; }
        public ICommand RemoveEventCommand { get; set; }
        public ICommand UpdateEventCommand { set; get; }
        

        public EditEventViewModel()
        {
            Events=EventManagerSingleton.Instance;
            GotoEventCommand = new RelayCommand(GotEventPage);
            RemoveEventCommand = new RelayCommand(RemoveEvent, SelectedIndexNotSet);
            UpdateEventCommand = new RelayCommand(UpdateEvent, SelectedIndexNotSet);
        }


        public bool SelectedIndexNotSet()
        {
            return SelectedIndex != -1;
        }

        public void GotEventPage()
        {
            MainViewModel.Instance.NavigateToPage(typeof(CreateEventPage));
        }

        public void RemoveEvent()
        {
            Events.RemoveAt(SelectedIndex);
            Save();
        }

        public void UpdateEvent()
        {
            Events.Update(SelectedIndex, SelectedEvent);
        }

        public async void Save()
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
