namespace KataBankOCR.Test.Tests
{
    using System;
    using System.IO;
    using Logic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NFluent;

    [TestClass]
    public class UserStory1Test
    {
        private static readonly TextToAccountNumberConverter Converter = new TextToAccountNumberConverter();

        [TestMethod]
        [DeploymentItem("Tests/UserStory1TestCases", "Ressources1")]
        public void GivenUseCase1TestCasesWhenConvertThenReturnStringMathcingFileName()
        {
            const string directoryPath = "Ressources1";
            foreach (var filePath in Directory.GetFiles(directoryPath))
            {
                var file = new FileInfo(filePath);
                var text = File.ReadAllText(file.FullName);
                var conversionResult = Converter.Convert(text);
                Check.That(conversionResult.Number).IsEqualTo(Path.GetFileNameWithoutExtension(file.Name));
            }
        }

        [TestMethod]
        [Ignore]
        [DeploymentItem("Tests/UserStory1TestCases/123456789.txt", "Ressources1")]
        public void GivenUseCase1TxtWhenConvertThenReturn000000000()
        {
            const string path = "Ressources1/123456789.txt";
            var text = File.ReadAllText(path);
            var textConverter = new TextToAccountNumberConverter();
            var checkedText = textConverter.Convert(text);
            Check.That(checkedText).IsEqualTo("123456789");
        }
    }
}