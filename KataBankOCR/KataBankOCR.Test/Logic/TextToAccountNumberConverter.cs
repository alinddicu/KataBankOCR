namespace KataBankOCR.Test.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class TextToAccountNumberConverter
    {
        private const int SymbolsPerTextFileLine = 9;

        private readonly AccountNumberApproximator _approximator = new AccountNumberApproximator();

        public AccountNumber Convert(string text)
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
                .Select((groupInfo, index) => new IntermediateCharacterGroupingInfo(groupInfo, index))
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
            var accountNumber = string.Join(string.Empty, digits);

            return _approximator.Approximate(accountNumber);
        }

        private struct LinearDigitSymbol
        {
            private readonly string _symbol;

            public LinearDigitSymbol(IEnumerable<IntermediateCharacterGroupingInfo> isgs)
                : this()
            {
                _symbol = string.Join(string.Empty, isgs.SelectMany(o => o.GroupInformation.Select(o1 => o1.Character)).ToArray());
            }

            public string ToDigit()
            {
                if (!Symbol.SymbolToDigitMapping.ContainsKey(_symbol))
                {
                    return Symbol.IllegalCharacterReplacement;
                }

                return Symbol.SymbolToDigitMapping[_symbol];
            }

            public override string ToString()
            {
                return _symbol;
            }
        }

        private struct IntermediateCharacterGroupingInfo
        {
            public IntermediateCharacterGroupingInfo(
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