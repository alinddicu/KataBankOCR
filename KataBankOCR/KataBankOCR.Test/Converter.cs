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
                .Where(line => !line.Equals(string.Empty))
                .Select(line => line.ToCharArray().Select(c => c.ToString(CultureInfo.InvariantCulture)))
                .Select(line => line.Select((s, index) => new Small3CharacterGroupingInfo(s, index)))
                .Select(line => line.GroupBy(s => s.Index / 3))
                .SelectMany(e => e.Select(i => i.ToList()))
                .Select((o, index) => new Intermediate9CharacterGroupingInfo(o, index))
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

            public LinearDigitSymbol(IEnumerable<Intermediate9CharacterGroupingInfo> isgs) 
                : this()
            {
                _symbol = string.Join(string.Empty, isgs.SelectMany(o => o.GroupingInformation.Select(o1 => o1.Character)).ToArray());
            }

            public string ToDigit()
            {
                return LinearSymbolToDigit[_symbol];
            }
        }

        private struct Intermediate9CharacterGroupingInfo
        {
            public Intermediate9CharacterGroupingInfo(
                IEnumerable<Small3CharacterGroupingInfo> groupingInformation,
                int index)
                : this()
            {
                Index = index;
                GroupingInformation = groupingInformation;
            }

            public int Index { get; private set; }

            public IEnumerable<Small3CharacterGroupingInfo> GroupingInformation { get; private set; }
        }

        private struct Small3CharacterGroupingInfo
        {
            public Small3CharacterGroupingInfo(
                string character,
                int index)
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
