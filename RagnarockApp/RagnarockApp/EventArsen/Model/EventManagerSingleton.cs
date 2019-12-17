using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Automation;
using RagnarockApp.Common;
using RagnarockApp.EventArsen.Exceptions;
using RagnarockApp.EventArsen.ViewModel;
using RagnarockApp.Persistency;
using RagnarockApp.QuizVictor.Exceptions;

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
            try
            {
                Validate(newEvent);
                Events.Add(newEvent);
            }
            catch (ValueEmptyException adex)
            {
                MessageDialogHelper.Show(adex.Message, "Event is not done being made");
            }
            catch (ValueAlreadyExistException adex)
            {
                MessageDialogHelper.Show(adex.Message, "Another Event already has that");
            }
        }

        public void RemoveAt(int index)
        {
            Events.RemoveAt(index);
        }

        public void Update(int index, Event eventToUpdate)
        {
            Events[index] = eventToUpdate;
        }

        private void Validate(Event eventAdd)
        {
            if (String.IsNullOrWhiteSpace(eventAdd.Title))
                throw new ValueEmptyException("The Event must have a title");
            if (String.IsNullOrWhiteSpace(eventAdd.EventSubject))
                throw new ValueEmptyException("The Event must have a subject");
            if (String.IsNullOrWhiteSpace(eventAdd.TimeOfDate))
                throw new ValueEmptyException("The Event must have a date");
            if (String.IsNullOrWhiteSpace(eventAdd.TimeOfEvent))
                throw new ValueEmptyException("The Event must have a time");
            if (String.IsNullOrWhiteSpace(eventAdd.EventImage))
                throw new ValueEmptyException("The Event must have an image");
            foreach (Event @event in Events)
            {
                if (@event.Title == eventAdd.Title)
                    throw new ValueAlreadyExistException("The title" + eventAdd.Title + "already exits");
                if (@event.TimeOfDate == eventAdd.TimeOfDate)
                    throw new ValueAlreadyExistException("The date" + eventAdd.TimeOfDate + "is taken");
                if (@event.TimeOfEvent == eventAdd.TimeOfEvent)
                    throw new ValueAlreadyExistException("The time" + eventAdd.TimeOfEvent + "already taken");
                if (@event.EventImage == eventAdd.EventImage)
                    throw new ValueAlreadyExistException("The Image is already taken");
            }
        }
    }
}
