using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace RagnarockApp.Converters
{
    public class IndexOfBoolArrayConverter : IValueConverter
    {
        private bool[] _array;
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // The value parameter is the data from the source object.
            _array = value as bool[];
            if (_array == null)
                return null;
            int index = Int32.Parse((string)parameter);
            return _array[index];
        }

        // ConvertBack is not implemented for a OneWay binding.
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (_array == null)
                return null;
            bool[] array = new bool[_array.Length];
            for (int i = 0; i < _array.Length; i++)
                array[i] = _array[i];
            int index = Int32.Parse((string)parameter);
            array[index] = (bool) value;
            return array;
        }
    }
}
