using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KataBankOCR.Test.Logic.Symbols
{
    public struct LinearDigitSymbol
    {
        private readonly string _symbol;

        public LinearDigitSymbol(IEnumerable<IntermediateCharacterGroupingInfo> isgs)
            : this()
        {
            _symbol = string.Join(string.Empty, isgs.SelectMany(o => o.GroupInformation.Select(o1 => o1.Character)).ToArray());
        }

        public string ToDigit()
        {
            if (!DigitSymbol.DigitSymbolToDigitMapping.ContainsKey(_symbol))
            {
                return DigitSymbol.IllegalCharacterReplacement;
            }

            return DigitSymbol.DigitSymbolToDigitMapping[_symbol];
        }

        public override string ToString()
        {
            return _symbol;
        }
    }
}