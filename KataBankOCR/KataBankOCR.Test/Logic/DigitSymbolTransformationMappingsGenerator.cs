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

            foreach (var characterWithIndexe in charactersWithIndexes)
            {
                var replacingCharacters = GetReplacingCharacters(characterWithIndexe.Character);
                foreach (var replacingCharacter in replacingCharacters)
                {
                    var transformationCandidate = symbol.WithCharAtIndex(replacingCharacter, characterWithIndexe.Index);
                    if (transformationCandidate.IsValid())
                    {
                        mapping.Transformations.Add(transformationCandidate);
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