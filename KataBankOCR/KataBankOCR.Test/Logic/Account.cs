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

        public ValidationStatus ValidationStatus { get; set; }

        public IEnumerable<Account> Approximations { get; private set; }

        public void UpdateApproximations(IEnumerable<Account> approximations)
        {
            Approximations = approximations;
            if (Approximations.Count() == 1)
            {
                Number = Approximations.First().Number;
                ValidationStatus = ValidationStatus.OK;
            }
            else
            {
                ValidationStatus = ValidationStatus.AMB;
            }
        }
    }
}