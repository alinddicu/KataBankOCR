namespace KataBankOCR.Test.Logic
{
    using System.Collections.Generic;
    using System.Text;

    public struct Symbol
    {
        public const string IllegalCharacterReplacement = "?";

        public static readonly Symbol Zero = new Symbol(
                                    " _ " +
                                    "| |" +
                                    "|_|");

        public static readonly Symbol One = new Symbol(
                                    "   " +
                                    "  |" +
                                    "  |");

        public static readonly Symbol Two = new Symbol(
                                    " _ " +
                                    " _|" +
                                    "|_ ");

        public static readonly Symbol Three = new Symbol(
                                    " _ " +
                                    " _|" +
                                    " _|");

        public static readonly Symbol Four = new Symbol(
                                    "   " +
                                    "|_|" +
                                    "  |");

        public static readonly Symbol Five = new Symbol(
                                    " _ " +
                                    "|_ " +
                                    " _|");

        public static readonly Symbol Six = new Symbol(
                                    " _ " +
                                    "|_ " +
                                    "|_|");

        public static readonly Symbol Seven = new Symbol(
                                    " _ " +
                                    "  |" +
                                    "  |");

        public static readonly Symbol Eight = new Symbol(
                                    " _ " +
                                    "|_|" +
                                    "|_|");

        public static readonly Symbol Nine = new Symbol(
                                    " _ " +
                                    "|_|" +
                                    " _|");

        public static readonly Dictionary<string, string> SymbolToDigitMapping = new Dictionary<string, string>
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

        public static readonly List<Symbol> AllSymbols = new List<Symbol> { Zero, One, Two, Three, Four, Five, Six, Seven, Eight, Nine };

        public Symbol(string linearForm)
            : this()
        {
            LinearForm = linearForm;
        }

        public string LinearForm { get; private set; }

        public bool IsValid()
        {
            return AllSymbols.Contains(this);
        }

        public Symbol WithCharAtIndex(char character, int index)
        {
            var sb = new StringBuilder(LinearForm);
            sb[index] = character;
            return new Symbol(sb.ToString());
        }

        public override string ToString()
        {
            return LinearForm;
        }

        public string ToDigit()
        {
            return SymbolToDigitMapping[LinearForm];
        }

        private bool Equals(Symbol other)
        {
            return LinearForm.Equals(other.LinearForm);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is Symbol && Equals((Symbol)obj);
        }

        public override int GetHashCode()
        {
            return LinearForm.GetHashCode();
        }

        public static bool operator ==(Symbol left, Symbol right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Symbol left, Symbol right)
        {
            return !(left == right);
        }
    }
}