namespace KataBankOCR.Test.Tests
{
    using System.IO;
    using Logic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NFluent;

    [TestClass]
    public class UserStory4Test
    {
        [TestMethod]
        [DeploymentItem("Tests/UserStory4TestCases/111111111.txt", "Ressources4")]
        public void GivenUseCase4v1TxtWhenConvertThenReturn711111111()
        {
            const string path = "Ressources4/111111111.txt";
            var text = File.ReadAllText(path);
            var textConverter = new TextToAccountNumberConverter();
            var checkedText = textConverter.Convert(text).Value;
            Check.That(checkedText).IsEqualTo("711111111");
        }
    }
}