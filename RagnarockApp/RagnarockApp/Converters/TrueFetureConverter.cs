using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace RagnarockApp.Converters
{
    public class TrueFetureConverter : IValueConverter
    {
        public object TrueValue { get; set; }
        public object FallbackValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // The value parameter is the data from the source object.
            int trueIndex;
            if (value is int)
                trueIndex = (int) value;
            else if (value is string)
                trueIndex = Int32.Parse((string) value);
            else
                return null;
            int index = Int32.Parse((string) parameter);

            if (trueIndex == index)
                return TrueValue;
            else
                return FallbackValue;
        }

        // ConvertBack is not implemented for a OneWay binding.
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}