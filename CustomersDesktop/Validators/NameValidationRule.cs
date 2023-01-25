using CustomersCore.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;

namespace CustomersDesktop.Validators
{
    public class NameValidationRule : ValidationRule
    {
        public IEnumerable<CustomerModel> Customers { get; set; } = Enumerable.Empty<CustomerModel>();
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (Customers.Any(c=>c.Name == (string)value))
            {
                return new ValidationResult(false,
                    $"Please enter unique name.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
