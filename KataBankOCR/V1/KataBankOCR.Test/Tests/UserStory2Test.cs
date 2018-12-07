namespace KataBankOCR.Test.Tests
{
    using Logic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NFluent;

    [TestClass]
    public class UserStory2Test
    {
        [TestMethod]
        public void Given345882865AsAccountNumberWhenValidateThenReturnTrue()
        {
            var validator = new AccountNumberChecksumValidator();

            var result = validator.Validate("345882865");

            Check.That(result).IsTrue();
        }

        [TestMethod]
        public void Given100000003AsAccountNumberWhenValidateThenReturnFalse()
        {
            var validator = new AccountNumberChecksumValidator();

            var result = validator.Validate("100000003");

            Check.That(result).IsFalse();
        }

        [TestMethod]
        public void Given123456789AsAccountNumberWhenValidateThenReturnTrue()
        {
            var validator = new AccountNumberChecksumValidator();

            var result = validator.Validate("123456789");

            Check.That(result).IsTrue();
        }
    }
}