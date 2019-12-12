using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Newtonsoft.Json;
using RagnarockApp.Common;
using RagnarockApp.EventArsen.Model;
using RagnarockApp.QuizVictor.Model;
using RagnarockApp.UserNicolai.Model;

namespace RagnarockApp.Persistency
{
    public class PersistencyFacade
    {
        //EventManager Saver
        private const string EventsFileName = "EventsJson.dat";

        public static async Task SaveEventsAsJsonAsync(ObservableCollection<Event> objectToSave)
        {
            string studentsJsonString = JsonConvert.SerializeObject(objectToSave);
            await SerializeObjectFileAsync(studentsJsonString, EventsFileName);
        }

        public static async Task<ObservableCollection<Event>> LoadEventsFromJsonAsync()
        {
            string objectJsonString = await DeSerializeObjectFileAsync(EventsFileName);
            if (objectJsonString != null)
                return (ObservableCollection<Event>)JsonConvert.DeserializeObject(objectJsonString, typeof(ObservableCollection<Event>));
            return null;
        }

        //UserCatalog Singleton Saver
        private const string UsersFileName = "UsersJson.dat";

        public static async Task SaveUsersAsJsonAsync(ObservableCollection<User> objectToSave)
        {
            string studentsJsonString = JsonConvert.SerializeObject(objectToSave);
            await SerializeObjectFileAsync(studentsJsonString, UsersFileName);
        }

        public static async Task<ObservableCollection<User>> LoadUsersFromJsonAsync()
        {
            string objectJsonString = await DeSerializeObjectFileAsync(UsersFileName);
            if (objectJsonString != null)
                return (ObservableCollection<User>)JsonConvert.DeserializeObject(objectJsonString, typeof(ObservableCollection<User>));
            return null;
        }

        //QuizPlayer Saver
        private const string QuizzesFileName = "QuizzesJson.dat";

        public static async Task SaveQuizzesAsJsonAsync(List<Quiz> objectToSave)
        {
            string studentsJsonString = JsonConvert.SerializeObject(objectToSave);
            await SerializeObjectFileAsync(studentsJsonString, QuizzesFileName);
        }

        public static async Task<List<Quiz>> LoadQuizzesFromJsonAsync()
        {
            string objectJsonString = await DeSerializeObjectFileAsync(QuizzesFileName);
            if (objectJsonString != null)
                return (List<Quiz>) JsonConvert.DeserializeObject(objectJsonString, typeof(List<Quiz>));
            return null;
        }


        //General Serialize and DeSerialize functions
        private static async Task SerializeObjectFileAsync(string objectString, string fileName)
        {
            StorageFile localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(localFile, objectString);
        }

        private static async Task<string> DeSerializeObjectFileAsync(string fileName)
        {
            try
            {
                StorageFile localFile = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return await FileIO.ReadTextAsync(localFile);
            }
            catch (FileNotFoundException)
            {

                MessageDialogHelper.Show("Loading for the first time?", "File not found!");
                return null;
            }
        }
    }
}
