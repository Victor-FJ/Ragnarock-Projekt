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

//namespace RagnarockApp.EventArsen.ViewModel
//{
//    public class EventViewModel : INotifyPropertyChanged
//    {
//        public EventManagerSingleton Events { get; set; }

//        private Event _selectedEvent;

//        public Event SelectedEvent
//        {
//            get { return _selectedEvent; }
//            set
//            {
//                _selectedEvent = value;
//                if (value == null)
//                {
//                    _selectedEvent = new Event();
//                }
//            }
//        }

//        public int SelectedIndex { get; set; }


//        private ICommand _addCommand;

//        public ICommand AddCommand
//        {
//            get { return _addCommand; }
//            set { _addCommand = value; }
//        }

//        public EventViewModel()
//        {
//            Events = EventManagerSingleton.Instance;
//            _removeCommand = new RelayCommand(RemoveEvent, SelectedIndexNotSet);
//            _updateCommand = new RelayCommand(UpdateEvent, SelectedIndexNotSet);
//        }

//        public bool SelectedIndexNotSet()
//        {
//            return SelectedIndex != -1;
//        }

//        public void RemoveEvent()
//        {
//            Events.RemoveAt(SelectedIndex);
//        }

//        public void UpdateEvent()
//        {
//            Events.Update(SelectedIndex, SelectedEvent);
//        }

//        public event PropertyChangedEventHandler PropertyChanged;

//        [NotifyPropertyChangedInvocator]
//        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }
//    }
//}
