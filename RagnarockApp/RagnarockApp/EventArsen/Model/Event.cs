using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RagnarockApp.EventArsen.Model
{
    public class Event
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Factor { get; set; }

        public Event()
        {
            Name = "";
            Date = "";
            Time = "";
            Factor = "";
        }

        public Event(string name, string date, string time, string factor)
        {
            Name = name;
            Date = date;
            Time = time;
            Factor = factor;
        }

        public override string ToString()
        {
            return $"{Name} {Date} {Time} {Factor}";
        }
    }
}
