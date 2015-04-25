namespace KataBankOCR.Test
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class Converter
    {
        private static readonly Dictionary<string, string> LinearSymbolToDigit = new Dictionary<string, string>
        {
            {" _ | ||_|", "0"}
        };

        public string Convert(string text)
        {
            var lines = text.Split(Environment.NewLine.ToCharArray());
            var digits = lines
                .Where(l => !l.Equals(string.Empty))
                .Select(l => l.ToCharArray().Select(c => c.ToString(CultureInfo.InvariantCulture)))
                .Select(l => l.Select((s, index) => new CharacterGroupBy3(index, s)))
                .Select(l => l.GroupBy(e => e.Index / 3)).ToArray()
                .SelectMany(e => e.Select(i => i.ToList()))
                .Select((o, index) => new IntermediateSymbolGroup(index, o))
                .GroupBy(o => o.Index % 9)
                .ToDictionary(o => o.Key)
                .Values
                .Select(v => new LinearDigitSymbol(v))
                .Select(ls => ls.ToDigit())
                .ToArray();

            return string.Join(string.Empty, digits);
        }

        private struct LinearDigitSymbol
        {
            private readonly string _symbol;

            public LinearDigitSymbol(IEnumerable<IntermediateSymbolGroup> isgs) 
                : this()
            {
                _symbol = string.Join(string.Empty, isgs.SelectMany(o => o.CharacterGroups.Select(o1 => o1.Character)).ToArray());
            }

            public string ToDigit()
            {
                return LinearSymbolToDigit[_symbol];
            }
        }

        private struct IntermediateSymbolGroup
        {
            public IntermediateSymbolGroup(int index, IEnumerable<CharacterGroupBy3> characterGroups)
                : this()
            {
                Index = index;
                CharacterGroups = characterGroups;
            }

            public int Index { get; private set; }

            public IEnumerable<CharacterGroupBy3> CharacterGroups { get; private set; }
        }

        private struct CharacterGroupBy3
        {
            public CharacterGroupBy3(int index, string character)
                : this()
            {
                Index = index;
                Character = character;
            }

            public int Index { get; private set; }

            public string Character { get; private set; }
        }
    }
}
