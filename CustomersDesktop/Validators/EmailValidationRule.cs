using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace CustomersDesktop.Validators
{
    public class EmailValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!Regex.IsMatch((string)value, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
            {
                return new ValidationResult(false,
                    "Please enter a valid email");
            }
            return ValidationResult.ValidResult;
        }
    }
}
