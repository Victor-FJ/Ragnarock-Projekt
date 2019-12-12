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
using RagnarockApp.QuizVictor.Model;

namespace RagnarockApp.Persistency
{
    public class PersistencyFacade
    {

        //QuizPlayer Saver
        private const string FileName = "QuizzesJson.dat";

        public static async Task SaveQuizzesAsJsonAsync(List<Quiz> objectToSave)
        {
            string studentsJsonString = JsonConvert.SerializeObject(objectToSave);
            await SerializeObjectFileAsync(studentsJsonString, FileName);
        }

        public static async Task<List<Quiz>> LoadQuizzesFromJsonAsync()
        {
            string objectJsonString = await DeSerializeObjectFileAsync(FileName);
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
