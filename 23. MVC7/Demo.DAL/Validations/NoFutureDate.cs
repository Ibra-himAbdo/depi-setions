namespace Demo.DAL.Validations
{
    public class NoFutureDate : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is not DateTime dateTime)
                return true;

            return dateTime <= DateTime.Now;
        }
    }
}
