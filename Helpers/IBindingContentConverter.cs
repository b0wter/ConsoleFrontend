using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ConsoleFrontend.Helpers
{
    /// <summary>
    /// Provides a way to apply custom logic to a binding.
    /// </summary>
    /// <remarks>
    /// Basically a copy if the IValueConverter from WPF.
    /// </remarks>
    public interface IBindingContentConverter
    {
        /// <summary>
        /// Converts the value of the binding source to a displayable string.
        /// </summary>
        /// <param name="value">The value that is produced by the binding source.</param>
        /// <param name="parameter">Converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns></returns>
        string Convert(object value, object parameter, CultureInfo culture);

        /// <summary>
        /// Converts the value of the binding target (which always is a string) back to its original type.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="parameter">Converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns></returns>
        object ConvertBack(string value, object parameter, CultureInfo culture);
    }
}
