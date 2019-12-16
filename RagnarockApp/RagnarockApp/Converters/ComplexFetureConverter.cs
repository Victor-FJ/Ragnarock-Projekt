using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace RagnarockApp.Converters
{
    public class ComplexFetureConverter : IValueConverter
    {
        public object Value1 { get; set; }
        public object Value2 { get; set; }
        public object Value3 { get; set; }
        public object FallbackValue { get; set; }


        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // The value parameter is the data from the source object.
            int[] _array = value as int[];
            if (_array == null)
                return null;
            int index = Int32.Parse((string)parameter);
            if (_array[index] == 1)
                return Value1;
            if (_array[index] == 2)
                return Value2;
            if (_array[index] == 3)
                return Value3;
            return FallbackValue;
        }

        // ConvertBack is not implemented for a OneWay binding.
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
