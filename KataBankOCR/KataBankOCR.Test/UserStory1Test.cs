namespace KataBankOCR.Test
{
    using System;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NFluent;

    [TestClass]
    public class UserStory1Test
    {
        private static readonly Converter TextConverter = new Converter();

        [TestMethod]
        [DeploymentItem("UserStory1TestCases", "Ressources")]
        public void GivenUseCase1TestCasesWhenConvertThenReturnStringMathcingFileName()
        {
            const string directoryPath = "Ressources";
            foreach (var filePath in Directory.GetFiles(directoryPath))
            {
                var file = new FileInfo(filePath);
                var text = File.ReadAllText(file.FullName);
                Check.That(TextConverter.Convert(text)).IsEqualTo(Path.GetFileNameWithoutExtension(file.Name));
            }
        }

        [TestMethod]
        [Ignore]
        [DeploymentItem("UserStory1TestCases/123456789.txt", "Ressources")]
        public void GivenUseCase1TxtWhenConvertThenReturn000000000()
        {
            const string path = "Ressources/123456789.txt";
            var text = File.ReadAllText(path);
            var textConverter = new Converter();
            var checkedText = textConverter.Convert(text);
            Check.That(checkedText).IsEqualTo("123456789");
        }
    }
}