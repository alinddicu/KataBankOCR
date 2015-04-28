namespace KataBankOCR.Test.Tests
{
    using System.Linq;
    using Logic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NFluent;

    [TestClass]
    public class SymbolTransformationMappingsGeneratorTest
    {
        private static readonly SymbolTransformationMappingsGenerator _generator = new SymbolTransformationMappingsGenerator();

        [TestMethod]
        public void When1ThenMappingContainsOnly7()
        {
            var results = _generator.Generate();

            Check.That(results.Single(r => r.Symbol == Symbol.One).Transformations).ContainsExactly(Symbol.Seven);
        }

        [TestMethod]
        public void When7ThenMappingContainsOnly1()
        {
            var results = _generator.Generate().ToList();

            Check.That(results.Single(r => r.Symbol == Symbol.Seven).Transformations).ContainsExactly(Symbol.One);
        }

        [TestMethod]
        public void When6ThenMappingContains5And8()
        {
            var result = _generator.Generate().Single(r => r.Symbol == Symbol.Six);

            Check.That(result.Transformations).HasSize(2);
            Check.That(result.Transformations).Contains(Symbol.Five, Symbol.Eight);
        }

        [TestMethod]
        public void WhenGenerateAllThenReturn8Mappings()
        {
            var results = _generator.Generate();

            Check.That(results).HasSize(8);
        }
    }
}