namespace KataBankOCR.Test.Logic.Symbols
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public struct DigitSymbol
    {
        public const string IllegalCharacterReplacement = "?";

        private static readonly DigitSymbol Zero = new DigitSymbol(
                                    " _ " +
                                    "| |" +
                                    "|_|");

        public static readonly DigitSymbol One = new DigitSymbol(
                                    "   " +
                                    "  |" +
                                    "  |");

        private static readonly DigitSymbol Two = new DigitSymbol(
                                    " _ " +
                                    " _|" +
                                    "|_ ");

        private static readonly DigitSymbol Three = new DigitSymbol(
                                    " _ " +
                                    " _|" +
                                    " _|");

        private static readonly DigitSymbol Four = new DigitSymbol(
                                    "   " +
                                    "|_|" +
                                    "  |");

        public static readonly DigitSymbol Five = new DigitSymbol(
                                    " _ " +
                                    "|_ " +
                                    " _|");

        public static readonly DigitSymbol Six = new DigitSymbol(
                                    " _ " +
                                    "|_ " +
                                    "|_|");

        public static readonly DigitSymbol Seven = new DigitSymbol(
                                    " _ " +
                                    "  |" +
                                    "  |");

        public static readonly DigitSymbol Eight = new DigitSymbol(
                                    " _ " +
                                    "|_|" +
                                    "|_|");

        private static readonly DigitSymbol Nine = new DigitSymbol(
                                    " _ " +
                                    "|_|" +
                                    " _|");

        private static readonly Dictionary<string, string> DigitSymbolToDigitMapping = new Dictionary<string, string>
        {
            {Zero.ToString(), "0"},
            {One.ToString(), "1"},
            {Two.ToString(), "2"},
            {Three.ToString(), "3"},
            {Four.ToString(), "4"},
            {Five.ToString(), "5"},
            {Six.ToString(), "6"},
            {Seven.ToString(), "7"},
            {Eight.ToString(), "8"},
            {Nine.ToString(), "9"},
        };

        public static readonly List<DigitSymbol> AllSymbols = new List<DigitSymbol> { Zero, One, Two, Three, Four, Five, Six, Seven, Eight, Nine };

        public DigitSymbol(IEnumerable<IntermediateCharacterGroupingInfo> isgs)
            : this()
        {
            LinearForm = string.Join(string.Empty, isgs.SelectMany(o => o.GroupInformation.Select(o1 => o1.Character)).ToArray());
        }

        public DigitSymbol(string linearForm)
            : this()
        {
            LinearForm = linearForm;
        }

        public string LinearForm { get; private set; }

        public bool IsValid()
        {
            return AllSymbols.Contains(this);
        }

        public DigitSymbol WithCharAtIndex(char character, int index)
        {
            var sb = new StringBuilder(LinearForm);

            sb[index] = character;

            return new DigitSymbol(sb.ToString());
        }

        public override string ToString()
        {
            return LinearForm;
        }

        public string ToDigit()
        {
            return !DigitSymbolToDigitMapping.ContainsKey(LinearForm) 
                ? IllegalCharacterReplacement 
                : DigitSymbolToDigitMapping[LinearForm];
        }

        private bool Equals(DigitSymbol other)
        {
            return LinearForm.Equals(other.LinearForm);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is DigitSymbol && Equals((DigitSymbol)obj);
        }

        public override int GetHashCode()
        {
            return LinearForm.GetHashCode();
        }

        public static bool operator ==(DigitSymbol left, DigitSymbol right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(DigitSymbol left, DigitSymbol right)
        {
            return !(left == right);
        }
    }
}