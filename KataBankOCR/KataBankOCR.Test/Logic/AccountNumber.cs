namespace KataBankOCR.Test.Logic
{
    public class AccountNumber
    {
        public AccountNumber(string value)
        {
            Value = value;
        }

        public AccountNumber(string value, ValidationResult validationResult)
            : this(value)
        {
            ValidationResult = validationResult;
        }

        public string Value { get; private set; }

        public ValidationResult ValidationResult { get; private set; }
    }
}