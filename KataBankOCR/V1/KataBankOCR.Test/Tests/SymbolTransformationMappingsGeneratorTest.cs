namespace KataBankOCR.Test.Tests
{
    using System.Linq;
    using Logic;
    using Logic.Symbols;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NFluent;

    [TestClass]
    public class SymbolTransformationMappingsGeneratorTest
    {
        private static readonly DigitSymbolTransformationMappingsGenerator Generator = new DigitSymbolTransformationMappingsGenerator();

        [TestMethod]
        public void When1ThenMappingContainsOnly7()
        {
            var results = Generator.Generate();

            Check.That(results.Single(r => r.Symbol == DigitSymbol.One).Transformations).ContainsExactly(DigitSymbol.Seven);
        }

        [TestMethod]
        public void When7ThenMappingContainsOnly1()
        {
            var results = Generator.Generate().ToList();

            Check.That(results.Single(r => r.Symbol == DigitSymbol.Seven).Transformations).ContainsExactly(DigitSymbol.One);
        }

        [TestMethod]
        public void When6ThenMappingContains5And8()
        {
            var result = Generator.Generate().Single(r => r.Symbol == DigitSymbol.Six);

            Check.That(result.Transformations).HasSize(2);
            Check.That(result.Transformations).Contains(DigitSymbol.Five, DigitSymbol.Eight);
        }

        [TestMethod]
        public void WhenGenerateAllThenReturn8Mappings()
        {
            var results = Generator.Generate();

            Check.That(results).HasSize(8);
        }
    }
}