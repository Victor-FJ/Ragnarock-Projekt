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
            int index = Int32.Parse((string)parameter);
            if (_array == null)
                return null;
            return _array[index];
        }

        // ConvertBack is not implemented for a OneWay binding.
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (_array == null)
                return null;
            int index = Int32.Parse((string)parameter);
            _array[index] = (bool) value;
            return _array;
        }
    }
}
