namespace KataBankOCR.Test.Logic
{
    public class AccountNumberApproximator
    {
        private readonly AccountNumberChecksumValidator _checksumValidator = new AccountNumberChecksumValidator();

        public AccountNumber Approximate(string accountNumber)
        {
            var tempAccountNumber = new AccountNumber(accountNumber);

            if (accountNumber.Contains(TextToAccountNumberConverter.IllegalCharacterReplacement))
            {
                tempAccountNumber.ValidationResult = ValidationStatus.ILL;
                return Approximate(tempAccountNumber);
            }

            if (!_checksumValidator.Validate(accountNumber))
            {
                tempAccountNumber.ValidationResult = ValidationStatus.ERR;
                return Approximate(tempAccountNumber);
            }

            return tempAccountNumber;
        }

        private AccountNumber Approximate(AccountNumber accountNumber)
        {
            return accountNumber;
        }
    }
}