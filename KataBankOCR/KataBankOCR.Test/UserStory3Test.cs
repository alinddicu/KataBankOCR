namespace KataBankOCR.Test
{
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NFluent;

    [TestClass]
    public class UserStory3Test
    {
        [TestMethod]
        [DeploymentItem("UserStory3TestCases/000000051.txt", "Ressources3")]
        public void GivenUseCase3v1TxtWhenConvertThenReturn000000051()
        {
            const string path = "Ressources3/000000051.txt";
            var text = File.ReadAllText(path);
            var textConverter = new Converter();
            var checkedText = textConverter.Convert(text);
            Check.That(checkedText).IsEqualTo("000000051");
        }

        [TestMethod]
        [DeploymentItem("UserStory3TestCases/49006771x.txt", "Ressources3")]
        public void GivenUseCase3v1TxtWhenConvertThenReturn49006771x()
        {
            const string path = "Ressources3/49006771x.txt";
            var text = File.ReadAllText(path);
            var textConverter = new Converter();
            var checkedText = textConverter.Convert(text);
            Check.That(checkedText).IsEqualTo("49006771?");
        }

        [TestMethod]
        [DeploymentItem("UserStory3TestCases/1234x678x.txt", "Ressources3")]
        public void GivenUseCase3v1TxtWhenConvertThenReturn1234x678x()
        {
            const string path = "Ressources3/1234x678x.txt";
            var text = File.ReadAllText(path);
            var textConverter = new Converter();
            var checkedText = textConverter.Convert(text);
            Check.That(checkedText).IsEqualTo("1234?678?");
        }
    }
}