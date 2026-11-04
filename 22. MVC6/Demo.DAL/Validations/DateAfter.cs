namespace Demo.DAL.Validations
{
    public class DateAfter : ValidationAttribute
    {
        private readonly string _otherPropertyName;
        public DateAfter(string otherPropertyName)
        {
            _otherPropertyName = otherPropertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not DateTime dateTime)
                return ValidationResult.Success;

            PropertyInfo? otherProperty = validationContext.ObjectType.GetProperty(_otherPropertyName);

            DateTime otherDateTime = Convert.ToDateTime(otherProperty?.GetValue(validationContext.ObjectInstance));

            if (otherDateTime > dateTime)
                return new ValidationResult($"{validationContext.MemberName} can't be samll than {_otherPropertyName}");

            return ValidationResult.Success;
        }

    }
}
