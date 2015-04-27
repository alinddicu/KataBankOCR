namespace KataBankOCR.Test.Tests
{
    using Logic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NFluent;

    [TestClass]
    public class SymbolTransformationMappingGeneratorTest
    {
        private static readonly SymbolTransformationMappingGenerator _generator = new SymbolTransformationMappingGenerator();

        [TestMethod]
        public void When1ThenMappingContainsOnly7()
        {
            var result = _generator.Generate(Symbol.One);

            Check.That(result.TransformationAlternatives).ContainsExactly(Symbol.Seven);
        }

        [TestMethod]
        public void When7ThenMappingContainsOnly1()
        {
            var result = _generator.Generate(Symbol.Seven);

            Check.That(result.TransformationAlternatives).ContainsExactly(Symbol.One);
        }

        [TestMethod]
        public void When6ThenMappingContains5And8()
        {
            var result = _generator.Generate(Symbol.Six);

            Check.That(result.TransformationAlternatives).HasSize(2);
            Check.That(result.TransformationAlternatives).Contains(Symbol.Five, Symbol.Eight);
        }

        [TestMethod]
        public void WhenGenerateAllThenReturn8Mappings()
        {
            var results = _generator.Generate();

            Check.That(results).HasSize(8);
        }
    }
}