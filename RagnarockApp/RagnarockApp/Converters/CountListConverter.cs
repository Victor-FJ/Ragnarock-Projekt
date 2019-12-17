using System;
using System.Collections;
using Windows.UI.Xaml.Data;

namespace RagnarockApp.Converters
{
    public class CountListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // The value parameter is the data from the source object.
            IList list = (IList)value;
            return list.Count;
        }

        // ConvertBack is not implemented for a OneWay binding.
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
