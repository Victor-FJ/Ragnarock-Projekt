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
        public string EventImage { get; set; }

        public Event()
        {
            Title = "";
            TimeOfDate = "";
            TimeOfEvent = "";
            EventSubject = "";
            EventImage = "";
        }

        public Event(string eventSubject, string title, string timeOfDate, string timeOfEvent, string eventImage)
        {
            Title = title;
            TimeOfDate = timeOfDate;
            TimeOfEvent = timeOfEvent;
            EventSubject = eventSubject;
            EventImage = eventImage;
        }

        public override string ToString()
        {
            return $"{EventSubject} {Title} {TimeOfDate} {TimeOfEvent}";
        }
    }
}
