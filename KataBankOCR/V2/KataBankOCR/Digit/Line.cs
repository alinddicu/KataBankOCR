namespace KataBankOCR.Digit
{
	public class Line
	{
		private readonly Element _left;
		private readonly Element _middle;
		private readonly Element _right;

		public Line(Element left, Element middle, Element right)
		{
			_left = left;
			_middle = middle;
			_right = right;
		}

		public override string ToString()
		{
			return "" + _left + _middle + _right;
		}
	}
}
