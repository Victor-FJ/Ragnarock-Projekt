using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace RagnarockApp.Converters
{
    public class TrueAmountConverter : IValueConverter
    {
        public object TrueValue { get; set; }
        public object FallbackValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // The value parameter is the data from the source object.
            IList list = (IList)value;
            if (list == null)
                return null;
            int amount = list.Count;
            int reqAmount = Int32.Parse((string)parameter);

            if (amount < reqAmount)
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
