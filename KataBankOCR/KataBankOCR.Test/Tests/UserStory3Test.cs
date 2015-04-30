namespace KataBankOCR.Test.Tests
{
    using System.IO;
    using Logic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NFluent;

    [TestClass]
    public class UserStory3Test
    {
        private static readonly TextToAccountConverter Converter = new TextToAccountConverter();

        [TestMethod]
        [DeploymentItem("Tests/UserStory3TestCases/000000051.txt", "Ressources3")]
        public void GivenUseCase3V1TxtWhenConvertThenReturn000000051()
        {
            var checkedText = LoadAndConvert("000000051");
            Check.That(checkedText).IsEqualTo("000000051");
        }

        [TestMethod]
        [DeploymentItem("Tests/UserStory3TestCases/49006771x.txt", "Ressources3")]
        public void GivenUseCase3V1TxtWhenConvertThenReturn49006771X()
        {
            var checkedText = LoadAndConvert("49006771x");
            Check.That(checkedText).IsEqualTo("49006771?");
        }

        [TestMethod]
        [DeploymentItem("Tests/UserStory3TestCases/1234x678x.txt", "Ressources3")]
        public void GivenUseCase3V1TxtWhenConvertThenReturn1234X678X()
        {
            var checkedText = LoadAndConvert("1234x678x");
            Check.That(checkedText).IsEqualTo("1234?678?");
        }

        private static string LoadAndConvert(string filePath)
        {
            var text = File.ReadAllText("Ressources3/" + filePath + ".txt");
            var textConverter = new TextToAccountConverter();

            return textConverter.Convert(text).Number;
        }
    }
}