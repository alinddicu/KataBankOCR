namespace KataBankOCR.Test.Logic
{
    using System.Collections.Generic;
    using System.Linq;
    using Symbols;

    public class DigitSymbolTransformationMappingsGenerator
    {
        public static readonly char[] ReplacingCharacters = new[] { ' ', '_', '|' };

        public IEnumerable<DigitSymbolTransformationMapping> Generate()
        {
            return DigitSymbol.AllSymbols
                .Select(Generate)
                .Where(mapping => mapping.Transformations.Any());
        }

        private DigitSymbolTransformationMapping Generate(DigitSymbol symbol)
        {
            var mapping = new DigitSymbolTransformationMapping(symbol);
            var charactersWithIndexes = symbol.LinearForm
                .ToArray()
                .Select((character, index) => new {Character = character, Index = index});

            foreach (var item in charactersWithIndexes)
            {
                var replacingCharacters = GetReplacingCharacters(item.Character);
                foreach (var replacingCharacter in replacingCharacters)
                {
                    var candidate = symbol.WithCharAtIndex(replacingCharacter, item.Index);
                    if (candidate.IsValid())
                    {
                        mapping.Transformations.Add(candidate);
                    }
                }
            }

            return mapping;
        }

        private static IEnumerable<char> GetReplacingCharacters(char reference)
        {
            return ReplacingCharacters.Except(new[] { reference });
        }
    }
}