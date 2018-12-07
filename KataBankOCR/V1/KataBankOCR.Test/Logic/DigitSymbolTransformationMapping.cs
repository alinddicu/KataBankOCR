namespace KataBankOCR.Test.Logic
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Symbols;

    public class DigitSymbolTransformationMapping
    {
        public DigitSymbolTransformationMapping(DigitSymbol symbol)
        {
            Symbol = symbol;
            Transformations = new List<DigitSymbol>();
        }

        public DigitSymbol Symbol { get; private set; }

        public List<DigitSymbol> Transformations { get; private set; }

        public override string ToString()
        {
            const string format = "Symbol: {0}, Transformations: [{1}]";
            var toString = string.Format(CultureInfo.InvariantCulture, format, Symbol.ToDigit(), string.Join(", ", Transformations.Select(t => t.ToDigit()).ToArray()));

            return toString;
        }
    }
}