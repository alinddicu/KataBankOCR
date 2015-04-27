namespace KataBankOCR.Test.Logic
{
    using System.Collections.Generic;

    public class SymbolTransformationMapping
    {
        public SymbolTransformationMapping(Symbol referenceSymbol)
        {
            ReferenceSymbol = referenceSymbol;
            TransformationAlternatives = new List<Symbol>();
        }

        public Symbol ReferenceSymbol { get; private set; }

        public List<Symbol> TransformationAlternatives { get; private set; }
    }
}