using System.Globalization;
using System.Windows.Controls;

namespace CustomersDesktop.Validators
{
    public class MaxLengthValidationRule : ValidationRule
    {
        public int MaxLength { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (((string)value).Length > MaxLength)
            {
                return new ValidationResult(false,
                    $"Max string length is {MaxLength}");
            }
            return ValidationResult.ValidResult;
        }
    }
}
