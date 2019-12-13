using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace RagnarockApp.Converters
{
    public class WeirdBoldConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // The value parameter is the data from the source object.
            if (value == null)
                return null;
            int boldIndex = (int) value;
            int index = Int32.Parse((string) parameter);
            if (boldIndex == index)
                return "Bold";
            else
                return "Normal";
        }

        // ConvertBack is not implemented for a OneWay binding.
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}