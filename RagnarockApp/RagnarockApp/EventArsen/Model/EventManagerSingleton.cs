using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RagnarockApp.EventArsen.Model
{
    public class EventManagerSingleton
    {
        public ObservableCollection<Event> Events { get; set; }

        private static EventManagerSingleton _instance = new EventManagerSingleton();

        public static EventManagerSingleton Instance
        {
            get { return _instance; }
        }

        private EventManagerSingleton()
        {
            Events = new ObservableCollection<Event>();
            Events.Add(new Event("Søren", "5/8", "17.00", "Koncert"));

        }

        public void Add(Event newEvent)
        {
            Events.Add(newEvent);
        }

        public void RemoveAt(int index)
        {
            Events.RemoveAt(index);
        }

        public void Update(int index, Event eventToUpdate)
        {
            Events[index] = eventToUpdate;
        }
    }
}
