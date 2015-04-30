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
        private readonly SymbolTransformationMapping[] _symbolTransformations = new SymbolTransformationMappingsGenerator().Generate().ToArray();

        public AccountNumber Approximate(string accountNumberValue, IEnumerable<LinearDigitSymbol> linearDigitSymbols)
        {
            var accountNumber = new AccountNumber(accountNumberValue);
            if (accountNumberValue.Contains(DigitSymbol.IllegalCharacterReplacement))
            {
                accountNumber.ValidationStatus = ValidationStatus.ILL;
                return ApproximateOnIllegal(accountNumber, linearDigitSymbols);
            }

            if (!_checksumValidator.Validate(accountNumberValue))
            {
                accountNumber.ValidationStatus = ValidationStatus.ERR;
                return ApproximateOnError(accountNumber);
            }

            return accountNumber;
        }

        private AccountNumber ApproximateOnIllegal(AccountNumber accountNumber, IEnumerable<LinearDigitSymbol> linearDigitSymbols)
        {
            var illegalPosition = accountNumber.Value.IndexOf(DigitSymbol.IllegalCharacterReplacement);
            var incompleteDigitSymbol = new DigitSymbol(linearDigitSymbols.ToArray()[illegalPosition].ToString());

            var approximations = new List<AccountNumber>();

            var characters = incompleteDigitSymbol.LinearForm.ToCharArray().Select(c => c.ToString()).ToArray();
            for (var index = 0; index < characters.Count(); index++)
            {
                var character = characters[index];
                var replacingCharacters = GetReplacingCharacters(character);
                foreach (var replacingCharacter in replacingCharacters)
                {
                    var candidateDigit = new DigitSymbol(incompleteDigitSymbol.LinearForm.ReplaceCharAtIndex(index, replacingCharacter));
                    if (candidateDigit.IsValid())
                    {
                        var candidateAccountNumberValue = accountNumber.Value.ReplaceCharAtIndex(index, candidateDigit.ToDigit());
                        if (_checksumValidator.Validate(candidateAccountNumberValue))
                        {
                            approximations.Add(new AccountNumber(candidateAccountNumberValue));
                        }
                    }
                }
            }

            accountNumber.UpdateApproximations(approximations);

            return accountNumber;
        }

        private IEnumerable<string> GetReplacingCharacters(string currentCharacter)
        {
            return SymbolTransformationMappingsGenerator
                .ReplacingCharacters
                .Select(c => c.ToString()).Except(new[] { currentCharacter });
        }

        private AccountNumber ApproximateOnError(AccountNumber accountNumber)
        {
            var approximations = new List<AccountNumber>();
            var digits = accountNumber.Value.ToCharArray().Select(c => c.ToString()).ToArray();

            for (var index = 0; index < digits.Count(); index++)
            {
                var digit = digits[index];
                var characterTransformations = GetDigitTransformations(digit);
                foreach (var transformation in characterTransformations)
                {
                    var approximation = accountNumber.Value.ReplaceCharAtIndex(index, transformation);
                    if (_checksumValidator.Validate(approximation))
                    {
                        approximations.Add(new AccountNumber(approximation));
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