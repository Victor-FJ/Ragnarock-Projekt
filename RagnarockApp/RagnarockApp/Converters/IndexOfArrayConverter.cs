using System;
using Windows.UI.Xaml.Data;

namespace RagnarockApp.Converters
{
    public class IndexOfArrayConverter : IValueConverter
    {
        private object[] array;
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // The value parameter is the data from the source object.
            array = value as object[];
            int index = Int32.Parse((string) parameter);
            if (array == null)
                return null;
            return array[index];
        }

        // ConvertBack is not implemented for a OneWay binding.
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (array == null)
                return null;
            int index = Int32.Parse((string)parameter);
            array[index] = value;
            return array;
        }
    }
}
