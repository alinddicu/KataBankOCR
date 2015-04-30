using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KataBankOCR.Test.Logic.Symbols
{
    public struct IntermediateCharacterGroupingInfo
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
}