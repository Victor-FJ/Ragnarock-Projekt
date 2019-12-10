using System;
using Windows.UI.Xaml.Data;

namespace RagnarockApp.Converters
{
    public class DateTimeOffsetToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // The value parameter is the data from the source object.
            DateTimeOffset date = (DateTimeOffset)value;
            return date.DateTime.ToShortDateString();
        }

        // ConvertBack is not implemented for a OneWay binding.
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
