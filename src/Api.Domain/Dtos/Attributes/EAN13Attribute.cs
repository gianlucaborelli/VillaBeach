using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Dtos.Attributes
{
    public class EAN13Attribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("Código de Barras é obrigatório.");

            string barcode = value.ToString();

            if (barcode.Length != 13 || !barcode.All(char.IsDigit))
                return new ValidationResult("Código de barras em formato invalido. Deve ser um código EAN-13 válido com 13 dígitos.");

            // Verificar se o código de barras segue a lógica de validação do EAN-13
            int sum = 0;
            for (int i = 0; i < barcode.Length - 1; i++)
            {
                int digit = int.Parse(barcode[i].ToString());
                sum += i % 2 == 0 ? digit : digit * 3;
            }

            int checkDigit = 10 - (sum % 10);
            if (checkDigit == 10)
                checkDigit = 0;

            var result = checkDigit == int.Parse(barcode[12].ToString());

            if (result)
                return ValidationResult.Success;
            else
                return new ValidationResult("Código de barras inválido.");
        }
    }
}