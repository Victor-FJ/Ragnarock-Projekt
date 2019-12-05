using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RagnarockApp.EventArsen.Model
{
    public class Event
    {
        public string Title { get; set; }
        public string TimeOfDate { get; set; }
        public string TimeOfEvent { get; set; }
        public string EventSubject { get; set; }

        public Event()
        {
            Title = "";
            TimeOfDate = "";
            TimeOfEvent = "";
            EventSubject = "";
        }

        public Event(string title, string timeOfDate, string timeOfEvent, string eventSubject)
        {
            Title = Title;
            TimeOfDate = timeOfDate;
            TimeOfEvent = timeOfEvent;
            EventSubject = EventSubject;
        }

        public override string ToString()
        {
            return $"{Title} {TimeOfDate} {TimeOfEvent} {EventSubject}";
        }
    }
}
