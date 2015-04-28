namespace KataBankOCR.Test.Logic
{
    using System;
    using System.Linq;

    public class AccountNumberChecksumValidator
    {
        public bool Validate(string accountNumber)
        {
            if (accountNumber.Contains(Symbol.IllegalCharacterReplacement))
            {
                return false;
            }

            accountNumber = string.Join(string.Empty, accountNumber.ToCharArray().Reverse().ToArray());

            var sum = accountNumber
                .ToCharArray()
                .Select(c => c.ToString())
                .Select((character, index) => new { Value = int.Parse(character), Index = index + 1 })
                .Sum(o => o.Index * o.Value);
            var checksum = sum % 11;

            return checksum == 0;
        }
    }
}