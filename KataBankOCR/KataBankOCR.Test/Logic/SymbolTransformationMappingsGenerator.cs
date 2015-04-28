namespace KataBankOCR.Test.Logic
{
    using System.Collections.Generic;
    using System.Linq;
    using NFluent;

    public class SymbolTransformationMappingsGenerator
    {
        private static readonly char[] ReplacingCharacters = new[] { ' ', '_', '|' };

        public IEnumerable<SymbolTransformationMapping> Generate()
        {
            foreach (var symbol in Symbol.AllSymbols)
            {
                var mapping = Generate(symbol);
                if (mapping.Transformations.Any())
                {
                    yield return mapping;
                }
            }
        }

        private SymbolTransformationMapping Generate(Symbol symbol)
        {
            var mapping = new SymbolTransformationMapping(symbol);

            foreach (var item in symbol.LinearForm.ToArray().Select((character, index) => new { Character = character, Index = index }))
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

        private IEnumerable<char> GetReplacingCharacters(char reference)
        {
            return ReplacingCharacters.Except(new[] { reference });
        }
    }
}