namespace KataBankOCR.Test.Logic
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class SymbolTransformationMapping
    {
        public SymbolTransformationMapping(Symbol referenceSymbol)
        {
            ReferenceSymbol = referenceSymbol;
            TransformationAlternatives = new List<Symbol>();
        }

        public Symbol ReferenceSymbol { get; private set; }

        public List<Symbol> TransformationAlternatives { get; private set; }

        public override string ToString()
        {
            var format = "ReferenceSymbol: {0}, TransformationAlternatives: [{1}]";
            var toString = string.Format(CultureInfo.InvariantCulture, format, ReferenceSymbol.ToDigit(), string.Join(", ", TransformationAlternatives.Select(t => t.ToDigit()).ToArray()));

            return toString;
        }
    }
}