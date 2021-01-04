using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace kck_projekt.Wpf
{
    public class NumberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!Int32.TryParse(value.ToString(),out _))
            {
                return new ValidationResult(false, "Nieodpowiednia wartość!");
            }
            return ValidationResult.ValidResult;
        }
    }

    public class DecimalValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            decimal outputValue ;
            if (!decimal.TryParse(value.ToString(), NumberStyles.Any, new CultureInfo("en-US"), out outputValue))
            {
                return new ValidationResult(false, "Nieodpowiednia wartość!");
            }
            return ValidationResult.ValidResult;
        }
    }

    public class EmptyStringValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (String.IsNullOrEmpty(value.ToString()))
            {
                return new ValidationResult(false, "To pole nie może być puste!");
            }
            Debug.WriteLine("Validation '" + value.ToString() +"'");
            return ValidationResult.ValidResult;
        }
    }
}
