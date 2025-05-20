using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskPro.Application.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EnumValueValidationAttribute : ValidationAttribute
    {
        private readonly Type _enumType;

        public EnumValueValidationAttribute(Type enumType)
        {
            _enumType = enumType;
        }

        public override bool IsValid(object? value)
        {
            if (value == null)
                return false;

            // Verifica que el tipo sea un enum
            if (!_enumType.IsEnum)
                throw new ArgumentException("El tipo debe ser un enum");

            // Valida que el valor esté definido en el enum
            return Enum.IsDefined(_enumType, value);
        }

        public override string FormatErrorMessage(string name)
        {
            return $"El valor proporcionado para '{name}' no es válido.";
        }
    }
}
