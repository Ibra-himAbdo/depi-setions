using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.DAL.Validations
{
    public class AfterDate : ValidationAttribute
    {
        private readonly string _otherProprityName;
        public AfterDate(string otherProprityName)
        {
            _otherProprityName = otherProprityName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not DateTime endDate)
                return ValidationResult.Success;

            PropertyInfo? otherProperty = validationContext.ObjectType.GetProperty(_otherProprityName);

            DateTime otherValue = Convert.ToDateTime(otherProperty?.GetValue(validationContext.ObjectInstance));

            if(otherValue > endDate)
            {
                return new ValidationResult("start date cant be grater than end date");
            }


            return ValidationResult.Success;
        }
    }
}
