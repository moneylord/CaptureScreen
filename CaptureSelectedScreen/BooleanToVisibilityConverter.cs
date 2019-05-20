using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace CaptureSelectedScreen
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        private bool invert = false;

        public bool Invert
        {
            get { return invert; }
            set { invert = value; }
        }


        #region IValueConverter Member
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool? valuerBoolean = value as bool?;
            if (valuerBoolean == null)
                return Visibility.Collapsed;
            if (Invert == true)
            {
                return valuerBoolean.Value == true ? Visibility.Collapsed : Visibility.Visible;
            }
            return valuerBoolean.Value == true ? Visibility.Visible : Visibility.Collapsed;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
