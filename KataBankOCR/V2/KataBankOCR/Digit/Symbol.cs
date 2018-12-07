namespace KataBankOCR.Digit
{
	using System;

	public class Symbol
	{
		private readonly Line _top;
		private readonly Line _middle;
		private readonly Line _bottom;

		public Symbol(Line top, Line middle, Line bottom)
		{
			_top = top;
			_middle = middle;
			_bottom = bottom;
		}

		public override string ToString()
		{
			return _top + Environment.NewLine + _middle + Environment.NewLine + _bottom;
		}
	}
}
