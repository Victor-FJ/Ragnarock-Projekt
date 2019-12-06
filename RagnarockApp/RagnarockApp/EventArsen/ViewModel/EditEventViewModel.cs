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

        private EventManagerSingleton _eventManagerSingleton;


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
                ((RelayCommand)_addCommand).RaiseCanExecuteChanged();
                ((RelayCommand)_removeCommand).RaiseCanExecuteChanged();
                ((RelayCommand)_updateCommand).RaiseCanExecuteChanged();
            }
        }

        public int SelectedIndex { get; set; }

        private ICommand _removeCommand;

        public ICommand RemoveCommand
        {
            get { return _removeCommand; }
            set { _removeCommand = value; }
        }

        private ICommand _updateCommand;

        public ICommand UpdateCommand
        {
            get { return _updateCommand; }
            set { _updateCommand = value; }
        }

        private ICommand _addCommand;

       

        public ICommand AddCommand
        {
            get { return _addCommand; }
            set { _addCommand = value; }
        }

        public EditEventViewModel()
        {
            Events=EventManagerSingleton.Instance;
            _eventManagerSingleton = EventManagerSingleton.Instance;
            _removeCommand = new RelayCommand(RemoveEvent, SelectedIndexNotSet);
            _updateCommand = new RelayCommand(UpdateEvent, SelectedIndexNotSet);
            _addCommand = new RelayCommand(Add, EventIsSelected);
            SelectedEvent = new Event();
        }

        public bool SelectedIndexNotSet()
        {
            return SelectedIndex != -1;
        }

        public void RemoveEvent()
        {
            _eventManagerSingleton.RemoveAt(SelectedIndex);
        }

        public void UpdateEvent()
        {
            _eventManagerSingleton.Update(SelectedIndex, SelectedEvent);
        }

        public bool EventIsSelected()
        {
            return SelectedEvent != null;
        }

        public void Add()
        {
            _eventManagerSingleton.Add(SelectedEvent);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
