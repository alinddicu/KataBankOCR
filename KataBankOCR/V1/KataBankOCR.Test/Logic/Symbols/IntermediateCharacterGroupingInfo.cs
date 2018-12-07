namespace KataBankOCR.Test.Logic.Symbols
{
    using System.Collections.Generic;

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