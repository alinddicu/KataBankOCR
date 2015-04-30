namespace KataBankOCR.Test.Logic
{
    using System.Collections.Generic;
    using System.Linq;
    using Symbols;

    public class AccountNumberApproximator
    {
        private readonly AccountNumberChecksumValidator _checksumValidator = new AccountNumberChecksumValidator();
        private readonly DigitSymbolTransformationMapping[] _symbolTransformations = new DigitSymbolTransformationMappingsGenerator().Generate().ToArray();

        public Account Approximate(string accountNumberValue, IEnumerable<DigitSymbol> linearDigitSymbols)
        {
            var accountNumber = new Account(accountNumberValue);
            if (_checksumValidator.Validate(accountNumberValue))
            {
                return accountNumber;
            }

            if (accountNumberValue.Contains(DigitSymbol.IllegalCharacterReplacement))
            {
                accountNumber.ValidationStatus = AccountValidationStatus.ILL;
                return ApproximateOnIllegal(accountNumber, linearDigitSymbols);
            }

            accountNumber.ValidationStatus = AccountValidationStatus.ERR;
            return ApproximateOnError(accountNumber);
        }

        private Account ApproximateOnIllegal(Account accountNumber, IEnumerable<DigitSymbol> linearDigitSymbols)
        {
            var illegalPosition = accountNumber.Number.IndexOf(DigitSymbol.IllegalCharacterReplacement, System.StringComparison.Ordinal);
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
                    if (!candidateDigit.IsValid())
                    {
                        continue;
                    }

                    var candidateAccountNumberValue = accountNumber.Number.ReplaceCharAtIndex(illegalPosition, candidateDigit.ToDigit());
                    if (_checksumValidator.Validate(candidateAccountNumberValue))
                    {
                        approximations.Add(new Account(candidateAccountNumberValue));
                    }
                }
            }

            accountNumber.UpdateApproximations(approximations);

            return accountNumber;
        }

        private static IEnumerable<string> GetReplacingCharacters(string currentCharacter)
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
                approximations.AddRange(
                    from digitTransformation in digitTransformations 
                    select accountNumber.Number.ReplaceCharAtIndex(index, digitTransformation) 
                    into approximation 
                    where _checksumValidator.Validate(approximation) 
                    select new Account(approximation));
            }

            accountNumber.UpdateApproximations(approximations);

            return accountNumber;
        }

        private IEnumerable<string> GetDigitTransformations(string digit)
        {
            var symbol = _symbolTransformations.SingleOrDefault(st => st.Symbol.ToDigit() == digit);

            return symbol == null 
                ? Enumerable.Empty<string>() 
                : symbol.Transformations.Select(t => t.ToDigit());
        }
    }
}