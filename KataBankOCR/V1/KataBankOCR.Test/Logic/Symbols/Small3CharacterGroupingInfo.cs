﻿namespace KataBankOCR.Test.Logic.Symbols
{
    public struct Small3CharacterGroupingInfo
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