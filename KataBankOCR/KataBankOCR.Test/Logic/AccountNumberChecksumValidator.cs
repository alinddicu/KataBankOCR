namespace KataBankOCR.Test.Logic
{
    using System.Linq;
    using Symbols;

    public class AccountNumberChecksumValidator
    {
        public bool Validate(string accountNumber)
        {
            if (accountNumber.Contains(DigitSymbol.IllegalCharacterReplacement))
            {
                return false;
            }

            accountNumber = string.Join(string.Empty, accountNumber.ToCharArray().Reverse().ToArray());

            var sum = accountNumber
                .ToStringArray()
                .Select((character, index) => new { Value = int.Parse(character), Index = index + 1 })
                .Sum(o => o.Index * o.Value);
            var checksum = sum % 11;

            return checksum == 0;
        }
    }
}