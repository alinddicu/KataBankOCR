using System.Collections.Generic;
using System.Text;

namespace KataBankOCR.Test.Logic
{
    public struct Symbol
    {
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

        public static bool IsValid(Symbol symbol)
        {
            return AllSymbols.Contains(symbol);
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
    }
}