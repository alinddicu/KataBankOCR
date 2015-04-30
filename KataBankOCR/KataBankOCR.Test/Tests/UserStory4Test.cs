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
        public void GivenUseCase4v1TxtWhenConvert111111111ThenReturn711111111()
        {
            const string path = "Ressources4/111111111.txt";
            var text = File.ReadAllText(path);

            var textConverter = new TextToAccountNumberConverter();
            var checkedText = textConverter.Convert(text).Number;

            Check.That(checkedText).IsEqualTo("711111111");
        }

        [TestMethod]
        [DeploymentItem("Tests/UserStory4TestCases/777777777.txt", "Ressources4")]
        public void GivenUseCase4v1TxtWhenConvert777777777ThenReturn777777177()
        {
            const string path = "Ressources4/777777777.txt";
            var text = File.ReadAllText(path);

            var textConverter = new TextToAccountNumberConverter();
            var checkedText = textConverter.Convert(text).Number;

            Check.That(checkedText).IsEqualTo("777777177");
        }

        [TestMethod]
        [DeploymentItem("Tests/UserStory4TestCases/222222222.txt", "Ressources4")]
        public void GivenUseCase4v1TxtWhenConvert222222222ThenReturn200800000()
        {
            const string path = "Ressources4/222222222.txt";
            var text = File.ReadAllText(path);

            var textConverter = new TextToAccountNumberConverter();
            var checkedText = textConverter.Convert(text).Number;

            Check.That(checkedText).IsEqualTo("200800000");
        }

        [TestMethod]
        [DeploymentItem("Tests/UserStory4TestCases/333333333.txt", "Ressources4")]
        public void GivenUseCase4v1TxtWhenConvert333333333ThenReturn333393333()
        {
            const string path = "Ressources4/333333333.txt";
            var text = File.ReadAllText(path);

            var textConverter = new TextToAccountNumberConverter();
            var checkedText = textConverter.Convert(text).Number;

            Check.That(checkedText).IsEqualTo("333393333");
        }

        [TestMethod]
        [DeploymentItem("Tests/UserStory4TestCases/888888888.txt", "Ressources4")]
        public void GivenUseCase4v1TxtWhenConvert888888888ThenReturnAMB()
        {
            const string path = "Ressources4/888888888.txt";
            var text = File.ReadAllText(path);
            var textConverter = new TextToAccountNumberConverter();

            var accountNumber = textConverter.Convert(text);

            Check.That(accountNumber.ValidationStatus).IsEqualTo(ValidationStatus.AMB);
            Check.That(accountNumber.Approximations).HasSize(3);
            Check.That(accountNumber.Approximations.Select(a => a.Number)).Contains("888886888", "888888880", "888888988");
        }

        [TestMethod]
        [DeploymentItem("Tests/UserStory4TestCases/0x0000051.txt", "Ressources4")]
        public void GivenUseCase4v1TxtWhenConvert0x0000051ThenReturn000000051()
        {
            const string path = "Ressources4/0x0000051.txt";
            var text = File.ReadAllText(path);
            var textConverter = new TextToAccountNumberConverter();

            var accountNumber = textConverter.Convert(text);

            Check.That(accountNumber.Number).IsEqualTo("000000051");
        }

        [TestMethod]
        [DeploymentItem("Tests/UserStory4TestCases/x23456789.txt", "Ressources4")]
        public void GivenUseCase4v1TxtWhenConvertx23456789ThenReturnILL()
        {
            const string path = "Ressources4/x23456789.txt";
            var text = File.ReadAllText(path);
            var textConverter = new TextToAccountNumberConverter();

            var accountNumber = textConverter.Convert(text);

            Check.That(accountNumber.Number).IsEqualTo("123456789");
        }

        [TestMethod]
        [DeploymentItem("Tests/UserStory4TestCases/49086771x.txt", "Ressources4")]
        public void GivenUseCase4v1TxtWhenConvertx23456789ThenReturn490867715()
        {
            const string path = "Ressources4/49086771x.txt";
            var text = File.ReadAllText(path);
            var textConverter = new TextToAccountNumberConverter();

            var accountNumber = textConverter.Convert(text);

            Check.That(accountNumber.Number).IsEqualTo("490867715");
        }
    }
}