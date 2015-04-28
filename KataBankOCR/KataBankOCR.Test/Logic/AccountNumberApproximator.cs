namespace KataBankOCR.Test.Logic
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class AccountNumberApproximator
    {
        private readonly AccountNumberChecksumValidator _checksumValidator = new AccountNumberChecksumValidator();
        private readonly SymbolTransformationMapping[] _symbolTransformations = new SymbolTransformationMappingsGenerator().Generate().ToArray();

        public AccountNumber Approximate(string accountNumberValue)
        {
            var accountNumber = new AccountNumber(accountNumberValue);
            if (accountNumberValue.Contains(Symbol.IllegalCharacterReplacement))
            {
                accountNumber.ValidationStatus = ValidationStatus.ILL;
                return Approximate(accountNumber);
            }

            if (!_checksumValidator.Validate(accountNumberValue))
            {
                accountNumber.ValidationStatus = ValidationStatus.ERR;
                return Approximate(accountNumber);
            }

            return accountNumber;
        }

        private AccountNumber Approximate(AccountNumber accountNumber)
        {
            var approximations = new List<AccountNumber>();
            var digits = accountNumber.Value.ToCharArray().Select(c => c.ToString()).ToArray();

            for (var index = 0; index < digits.Count(); index++)
            {
                var digit = digits[index];
                var characterTransformations = GetCharacterTransformations(digit);
                foreach (var transformation in characterTransformations)
                {
                    var sb = new StringBuilder(accountNumber.Value);
                    sb[index] = transformation[0];

                    var approximation = sb.ToString();
                    if (_checksumValidator.Validate(approximation))
                    {
                        approximations.Add(new AccountNumber(approximation));
                    }
                }
            }

            accountNumber.UpdateApproximations(approximations);

            return accountNumber;
        }

        private IEnumerable<string> GetCharacterTransformations(string digit)
        {
            var symbol = _symbolTransformations.SingleOrDefault(st => st.Symbol.ToDigit() == digit);
            if (symbol == null)
            {
                return Enumerable.Empty<string>();
            }

            return symbol.Transformations.Select(t => t.ToDigit());
        }
    }
}