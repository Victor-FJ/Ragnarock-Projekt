using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RagnarockApp.EventArsen.ViewModel;
using RagnarockApp.Persistency;

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
            //Events.Add(new Event("Udstilling", "Gasolin", "13/9 - 31/7","Hele dagen","/assets/Gasolin.jpg" )); 
            //Events.Add(new Event("Koncert", "Metallica", "11/12", "19:00 - 21:00","/assets/Metallica.jpg"));
        }

        public void Create(Event newEvent)
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
