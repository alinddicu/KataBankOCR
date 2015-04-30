namespace KataBankOCR.Test.Logic
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class SymbolTransformationMapping
    {
        public SymbolTransformationMapping(DigitSymbol referenceSymbol)
        {
            Symbol = referenceSymbol;
            Transformations = new List<DigitSymbol>();
        }

        public DigitSymbol Symbol { get; private set; }

        public List<DigitSymbol> Transformations { get; private set; }

        public override string ToString()
        {
            var format = "ReferenceSymbol: {0}, Transformations: [{1}]";
            var toString = string.Format(CultureInfo.InvariantCulture, format, Symbol.ToDigit(), string.Join(", ", Transformations.Select(t => t.ToDigit()).ToArray()));

            return toString;
        }
    }
}