using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Newtonsoft.Json;
using RagnarockApp.QuizVictor.Model;

namespace RagnarockApp.Common
{
    public class CountListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // The value parameter is the data from the source object.
            IList list = (IList)value;

            //List<object> list = JsonConvert.DeserializeObject<List<object>>(JsonConvert.SerializeObject(value));
            return list.Count;
        }

        // ConvertBack is not implemented for a OneWay binding.
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
