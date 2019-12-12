using System;
using Windows.UI.Xaml.Data;

namespace RagnarockApp.Converters
{
    public class IndexOfArrayConverter : IValueConverter
    {
        private string[] _array;
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // The value parameter is the data from the source object.
            _array = value as string[];
            if (_array == null)
                return null;
            int index = Int32.Parse((string) parameter);
            return _array[index];
        }

        // ConvertBack is not implemented for a OneWay binding.
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (_array == null)
                return null;
            string[] array = new string[_array.Length];
            for (int i = 0; i < _array.Length; i++)
                array[i] = _array[i];
            int index = Int32.Parse((string)parameter);
            array[index] = (string) value;
            return array;
        }
    }
}
