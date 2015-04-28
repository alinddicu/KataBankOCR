namespace KataBankOCR.Test.Logic
{
    using System.Collections.Generic;
    using System.Linq;

    public class AccountNumber
    {
        public AccountNumber(string value)
        {
            Value = value;
            Approximations = new List<AccountNumber>();
        }

        public string Value { get; private set; }

        public ValidationStatus ValidationStatus { get; set; }

        public IEnumerable<AccountNumber> Approximations { get; private set; }

        public void UpdateApproximations(IEnumerable<AccountNumber> approximations)
        {
            Approximations = approximations;
            if (Approximations.Count() == 1)
            {
                Value = Approximations.First().Value;
                ValidationStatus = ValidationStatus.OK;
            }
            else
            {
                ValidationStatus = ValidationStatus.AMB;
            }
        }
    }
}