namespace KataBankOCR.Test.Logic
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using KataBankOCR.Test.Logic.Symbols;

    public class AccountNumberApproximator
    {
        private readonly AccountNumberChecksumValidator _checksumValidator = new AccountNumberChecksumValidator();
        private readonly DigitSymbolTransformationMapping[] _symbolTransformations = new DigitSymbolTransformationMappingsGenerator().Generate().ToArray();

        public Account Approximate(string accountNumberValue, IEnumerable<DigitSymbol> linearDigitSymbols)
        {
            var accountNumber = new Account(accountNumberValue);
            if (accountNumberValue.Contains(DigitSymbol.IllegalCharacterReplacement))
            {
                accountNumber.ValidationStatus = AccountValidationStatus.ILL;
                return ApproximateOnIllegal(accountNumber, linearDigitSymbols);
            }

            if (!_checksumValidator.Validate(accountNumberValue))
            {
                accountNumber.ValidationStatus = AccountValidationStatus.ERR;
                return ApproximateOnError(accountNumber);
            }

            return accountNumber;
        }

        private Account ApproximateOnIllegal(Account accountNumber, IEnumerable<DigitSymbol> linearDigitSymbols)
        {
            var illegalPosition = accountNumber.Number.IndexOf(DigitSymbol.IllegalCharacterReplacement);
            var incompleteDigitSymbol = new DigitSymbol(linearDigitSymbols.ToArray()[illegalPosition].ToString());

            var approximations = new List<Account>();

            var characters = incompleteDigitSymbol.LinearForm.ToStringArray();
            for (var index = 0; index < characters.Count(); index++)
            {
                var character = characters[index];
                var replacingCharacters = GetReplacingCharacters(character);
                foreach (var replacingCharacter in replacingCharacters)
                {
                    var candidateDigit = new DigitSymbol(incompleteDigitSymbol.LinearForm.ReplaceCharAtIndex(index, replacingCharacter));
                    if (candidateDigit.IsValid())
                    {
                        var candidateAccountNumberValue = accountNumber.Number.ReplaceCharAtIndex(illegalPosition, candidateDigit.ToDigit());
                        if (_checksumValidator.Validate(candidateAccountNumberValue))
                        {
                            approximations.Add(new Account(candidateAccountNumberValue));
                        }
                    }
                }
            }

            accountNumber.UpdateApproximations(approximations);

            return accountNumber;
        }

        private IEnumerable<string> GetReplacingCharacters(string currentCharacter)
        {
            return DigitSymbolTransformationMappingsGenerator
                .ReplacingCharacters
                .Select(c => c.ToString()).Except(new[] { currentCharacter });
        }

        private Account ApproximateOnError(Account accountNumber)
        {
            var approximations = new List<Account>();
            var accountNumberDigits = accountNumber.Number.ToStringArray();

            for (var index = 0; index < accountNumberDigits.Count(); index++)
            {
                var digit = accountNumberDigits[index];
                var digitTransformations = GetDigitTransformations(digit);
                foreach (var digitTransformation in digitTransformations)
                {
                    var approximation = accountNumber.Number.ReplaceCharAtIndex(index, digitTransformation);
                    if (_checksumValidator.Validate(approximation))
                    {
                        approximations.Add(new Account(approximation));
                    }
                }
            }

            accountNumber.UpdateApproximations(approximations);

            return accountNumber;
        }

        private IEnumerable<string> GetDigitTransformations(string digit)
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