namespace KataBankOCR.Test.Logic
{
    using System.Collections.Generic;
    using System.Linq;

    public class Account
    {
        public Account(string number)
        {
            Number = number;
            Approximations = new List<Account>();
        }

        public string Number { get; private set; }

        public AccountValidationStatus ValidationStatus { get; set; }

        public IEnumerable<Account> Approximations { get; private set; }

        public void UpdateApproximations(ICollection<Account> approximations)
        {
            if (!approximations.Any())
            {
                return;
            }

            Approximations = approximations;
            if (Approximations.Count() == 1)
            {
                Number = Approximations.First().Number;
                ValidationStatus = AccountValidationStatus.OK;
            }
            else
            {
                ValidationStatus = AccountValidationStatus.AMB;
            }
        }
    }
}