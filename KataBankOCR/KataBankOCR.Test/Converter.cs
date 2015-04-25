namespace KataBankOCR.Test
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class Converter
    {
        private static readonly Dictionary<string, string> LinearSymbolToDigitMapping 
            = new Dictionary<string, string>
        {
            {" _ | ||_|", "0"}
        };

        public string Convert(string text)
        {
            var lines = text.Split(Environment.NewLine.ToCharArray());
            var digits = lines
                .Where(line => !line.Equals(string.Empty))
                // transforming to list of strings = easier to work with
                .Select(line => line.ToCharArray().Select(c => c.ToString(CultureInfo.InvariantCulture)))
                .Select(line => line.Select((character, index) => new Small3CharacterGroupingInfo(character, index)))
                // each symbol is formed of 3 columns => group of 3 chars
                .Select(line => line.GroupBy(s => s.Index / 3))
                .SelectMany(e => e.Select(i => i.ToList()))
                .Select((groupInfo, index) => new Intermediate9CharacterGroupingInfo(groupInfo, index))
                // each symbol is formed of 3 lines * 3 colums = 9 characters => group it
                .GroupBy(o => o.Index % 9)
                .ToDictionary(o => o.Key)
                // easy part
                .Values
                .Select(value => new LinearDigitSymbol(value))
                .Select(linearSymbol => linearSymbol.ToDigit())
                .ToArray();

            return string.Join(string.Empty, digits);
        }

        private struct LinearDigitSymbol
        {
            private readonly string _symbol;

            public LinearDigitSymbol(IEnumerable<Intermediate9CharacterGroupingInfo> isgs) 
                : this()
            {
                _symbol = string.Join(string.Empty, isgs.SelectMany(o => o.GroupInformation.Select(o1 => o1.Character)).ToArray());
            }

            public string ToDigit()
            {
                return LinearSymbolToDigitMapping[_symbol];
            }
        }

        private struct Intermediate9CharacterGroupingInfo
        {
            public Intermediate9CharacterGroupingInfo(
                IEnumerable<Small3CharacterGroupingInfo> groupInformation,
                int index)
                : this()
            {
                Index = index;
                GroupInformation = groupInformation;
            }

            public int Index { get; private set; }

            public IEnumerable<Small3CharacterGroupingInfo> GroupInformation { get; private set; }
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
