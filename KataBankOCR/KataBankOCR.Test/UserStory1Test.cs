namespace KataBankOCR.Test
{
    using System;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NFluent;

    [TestClass]
    public class UserStory1Test
    {
        [TestMethod]
        [DeploymentItem("UserStory1TestCases/UseCase1.txt", "Ressources")]
        public void GivenUseCase1TxtWhenConvertThenReturn000000000()
        {
            const string path = "Ressources/UseCase1.txt";
            var text  = File.ReadAllText(path);
            var textConverter = new Converter();
            Check.That(textConverter.Convert(text)).IsEqualTo("000000000");
        }
    }
}
