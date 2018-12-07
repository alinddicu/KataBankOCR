namespace KataBankOCR.Test.Tests
{
    using System.IO;
    using System.Linq;
    using Logic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NFluent;

    [TestClass]
    public class UserStory4Test
    {
        [TestMethod]
        [DeploymentItem("Tests/UserStory4TestCases/111111111.txt", "Ressources4")]
        public void GivenUseCase4V1TxtWhenConvert111111111ThenReturn711111111()
        {
            var account = LoadAndConvert("111111111");

            Check.That(account.Number).IsEqualTo("711111111");
        }

        [TestMethod]
        [DeploymentItem("Tests/UserStory4TestCases/777777777.txt", "Ressources4")]
        public void GivenUseCase4V1TxtWhenConvert777777777ThenReturn777777177()
        {
            var account = LoadAndConvert("777777777");

            Check.That(account.Number).IsEqualTo("777777177");
        }

        [TestMethod]
        [DeploymentItem("Tests/UserStory4TestCases/222222222.txt", "Ressources4")]
        public void GivenUseCase4V1TxtWhenConvert222222222ThenReturn200800000()
        {
            var account = LoadAndConvert("222222222");

            Check.That(account.Number).IsEqualTo("200800000");
        }

        [TestMethod]
        [DeploymentItem("Tests/UserStory4TestCases/333333333.txt", "Ressources4")]
        public void GivenUseCase4V1TxtWhenConvert333333333ThenReturn333393333()
        {
            var account = LoadAndConvert("333333333");

            Check.That(account.Number).IsEqualTo("333393333");
        }

        [TestMethod]
        [DeploymentItem("Tests/UserStory4TestCases/888888888.txt", "Ressources4")]
        public void GivenUseCase4V1TxtWhenConvert888888888ThenReturnAmb()
        {
            var account = LoadAndConvert("888888888");

            Check.That(account.ValidationStatus).IsEqualTo(AccountValidationStatus.AMB);
            Check.That(account.Approximations).HasSize(3);
            Check.That(account.Approximations.Select(a => a.Number)).Contains("888886888", "888888880", "888888988");
        }

        [TestMethod]
        [DeploymentItem("Tests/UserStory4TestCases/555555555.txt", "Ressources4")]
        public void GivenUseCase4V1TxtWhenConvert555555555ThenReturnAmb()
        {
            var account = LoadAndConvert("555555555");

            Check.That(account.ValidationStatus).IsEqualTo(AccountValidationStatus.AMB);
            Check.That(account.Approximations).HasSize(2);
            Check.That(account.Approximations.Select(a => a.Number)).Contains("555655555", "559555555");
        }

        [TestMethod]
        [DeploymentItem("Tests/UserStory4TestCases/0x0000051.txt", "Ressources4")]
        public void GivenUseCase4V1TxtWhenConvert0X0000051ThenReturn000000051()
        {
            var account = LoadAndConvert("0x0000051");

            Check.That(account.Number).IsEqualTo("000000051");
        }

        [TestMethod]
        [DeploymentItem("Tests/UserStory4TestCases/x23456789.txt", "Ressources4")]
        public void GivenUseCase4V1TxtWhenConvertx23456789ThenReturnIll()
        {
            var account = LoadAndConvert("x23456789");

            Check.That(account.Number).IsEqualTo("123456789");
        }

        [TestMethod]
        [DeploymentItem("Tests/UserStory4TestCases/49086771x.txt", "Ressources4")]
        public void GivenUseCase4V1TxtWhenConvertx23456789ThenReturn490867715()
        {
            var account = LoadAndConvert("49086771x");

            Check.That(account.Number).IsEqualTo("490867715");
        }

        private static Account LoadAndConvert(string filePath)
        {
            var text = File.ReadAllText("Ressources4/" + filePath + ".txt");
            var textConverter = new TextToAccountConverter();

            return textConverter.Convert(text);
        }
    }
}