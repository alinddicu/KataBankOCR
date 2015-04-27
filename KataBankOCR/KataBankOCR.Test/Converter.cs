namespace KataBankOCR.Test
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class Converter
    {
        private const int SymbolsPerTextFileLine = 9;

        private static readonly Dictionary<string, string> LinearSymbolToDigitMapping
            = new Dictionary<string, string>
        {
            {" _ | ||_|", "0"},
           //"     |  | "
           //"     |  | "
            {"     |  |", "1"},
           //" _  _||_ "
            {" _  _||_ ", "2"},
            {" _  _| _|", "3"},
            {"   |_|  |", "4"},
            {" _ |_  _|", "5"},
            {" _ |_ |_|", "6"},
            {" _   |  |", "7"},
            {" _ |_||_|", "8"},
            {" _ |_| _|", "9"},
        };

        public string Convert(string text)
        {
            var lines = text.Split(Environment.NewLine.ToCharArray());
            var symbols = lines
                .Where(line => !line.Equals(string.Empty))
                // transforming to list of strings = easier to work with
                .Select(line => line.ToCharArray().Select(c => c.ToString(CultureInfo.InvariantCulture)))
                .Select(line => line.Select((character, index) => new Small3CharacterGroupingInfo(character, index)))
                // each symbol is formed of 3 columns => group of 3 chars
                .Select(line => line.GroupBy(s => s.Index / 3))
                .SelectMany(e => e.Select(i => i.ToList()))
                .Select((groupInfo, index) => new Intermediate9CharacterGroupingInfo(groupInfo, index))
                // grouping vertically
                .GroupBy(o => o.Index % SymbolsPerTextFileLine)
                .ToDictionary(o => o.Key)
                // easy part
                .Values
                .Select(value => new LinearDigitSymbol(value))
                .ToArray();

            var digits = symbols
                .Select(linearSymbol => linearSymbol.ToDigit())
                .ToArray();

            return string.Join(string.Empty, digits);
        }

        private struct LinearDigitSymbol
        {
            private const string IllegalCharacterReplacement = "?";

            private readonly string _symbol;

            public LinearDigitSymbol(IEnumerable<Intermediate9CharacterGroupingInfo> isgs)
                : this()
            {
                _symbol = string.Join(string.Empty, isgs.SelectMany(o => o.GroupInformation.Select(o1 => o1.Character)).ToArray());
            }

            public string ToDigit()
            {
                if (!LinearSymbolToDigitMapping.ContainsKey(_symbol))
                {
                    return IllegalCharacterReplacement;
                }

                return LinearSymbolToDigitMapping[_symbol];
            }

            public override string ToString()
            {
                return _symbol;
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