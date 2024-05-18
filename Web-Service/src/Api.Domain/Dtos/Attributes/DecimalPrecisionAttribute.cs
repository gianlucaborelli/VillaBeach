using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Api.Domain.Dtos.Attributes
{
    /// <summary>
    /// Decimal precision validator data annotation.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DecimalPrecisionAttribute : ValidationAttribute
    {
        private readonly uint _decimalPrecision;

        public DecimalPrecisionAttribute(uint decimalPrecision)
        {
            _decimalPrecision = decimalPrecision;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("Valor é obrigatório.");

            if (value is decimal d && HasPrecision(d, _decimalPrecision))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Valor em formato inválido.");
            }

        }

        private static bool HasPrecision(decimal value, uint precision)
        {
            string valueStr = value.ToString(CultureInfo.InvariantCulture);
            int indexOfDot = valueStr.IndexOf('.');
            if (indexOfDot == -1)
            {
                return true;
            }

            return valueStr.Length - indexOfDot - 1 <= precision;
        }
    }
}