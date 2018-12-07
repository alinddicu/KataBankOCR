namespace KataBankOCR.Digit
{
	using System;
	using System.Linq;

	public class Element
	{
		private static readonly char[] AllowedCharacters = {' ', '|', '_'};

		private readonly char _character;

		public Element(char character)
		{
			if (!AllowedCharacters.Contains(character))
			{
				throw new ArgumentException($"Illegal character '{character}'");
			}

			_character = character;
		}

		public override string ToString()
		{
			return _character.ToString();
		}
	}
}
