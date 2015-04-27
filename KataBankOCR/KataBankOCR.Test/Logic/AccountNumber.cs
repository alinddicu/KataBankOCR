namespace KataBankOCR.Test.Logic
{
    public class AccountNumber
    {
        public AccountNumber(string value)
        {
            Value = value;
        }

        public AccountNumber(string value, ValidationStatus validationResult)
            : this(value)
        {
            ValidationResult = validationResult;
        }

        public string Value { get; private set; }

        public ValidationStatus ValidationResult { get; set; }
    }
}