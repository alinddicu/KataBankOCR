namespace KataBankOCR.Test.Logic
{
    public class AccountNumber
    {
        public AccountNumber(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }

        public ValidationStatus ValidationResult { get; set; }
    }
}