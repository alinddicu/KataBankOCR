namespace KataBankOCR.Test.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Symbols;

    public class TextToAccountConverter
    {
        private const int SymbolsPerTextFileLine = 9;

        private readonly AccountNumberApproximator _approximator = new AccountNumberApproximator();

        public Account Convert(string text)
        {
            var lines = text.Split(Environment.NewLine.ToCharArray());
            var linearSymbols = lines
                .Where(line => !line.Equals(string.Empty))
                // transforming to list of strings = easier to work with
                .Select(line => line.ToStringArray())
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
                .Select(value => new DigitSymbol(value));

            var digits = linearSymbols
                .Select(linearSymbol => linearSymbol.ToDigit())
                .ToArray();
            var accountNumber = string.Join(string.Empty, digits);

            return _approximator.Approximate(accountNumber, linearSymbols);
        }
    }
}