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
        private static readonly TextToAccountConverter Converter = new TextToAccountConverter();

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
    }
}