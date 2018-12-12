namespace KataBankOCR.Digit
{
	using System;

	public class Digit
	{
		private readonly Line _top;
		private readonly Line _middle;
		private readonly Line _bottom;

		public static readonly Digit[] Digits =
		{
			new Digit(
				new Line(new Element(' '), new Element('_'),new Element(' ')),
				new Line(new Element('|'), new Element(' '),new Element('|')),
				new Line(new Element('|'), new Element('_'),new Element('|'))),
			new Digit(
				new Line(new Element(' '), new Element(' '),new Element(' ')),
				new Line(new Element(' '), new Element(' '),new Element('|')),
				new Line(new Element(' '), new Element(' '),new Element('|'))),
			new Digit(
				new Line(new Element(' '), new Element('_'),new Element(' ')),
				new Line(new Element(' '), new Element('_'),new Element('|')),
				new Line(new Element('|'), new Element('_'),new Element(' '))),
			new Digit(
				new Line(new Element(' '), new Element('_'),new Element(' ')),
				new Line(new Element(' '), new Element('_'),new Element('|')),
				new Line(new Element(' '), new Element('_'),new Element('|'))),
			new Digit(
				new Line(new Element(' '), new Element(' '),new Element(' ')),
				new Line(new Element('|'), new Element('_'),new Element('|')),
				new Line(new Element(' '), new Element(' '),new Element('|'))),
			new Digit(
				new Line(new Element(' '), new Element('_'),new Element(' ')),
				new Line(new Element('|'), new Element('_'),new Element(' ')),
				new Line(new Element(' '), new Element('_'),new Element('|'))),
			new Digit(
				new Line(new Element(' '), new Element('_'),new Element(' ')),
				new Line(new Element('|'), new Element('_'),new Element(' ')),
				new Line(new Element('|'), new Element('_'),new Element('|'))),
			new Digit(
				new Line(new Element(' '), new Element('_'),new Element(' ')),
				new Line(new Element(' '), new Element(' '),new Element('|')),
				new Line(new Element(' '), new Element(' '),new Element('|'))),
			new Digit(
				new Line(new Element(' '), new Element('_'),new Element(' ')),
				new Line(new Element('|'), new Element('_'),new Element('|')),
				new Line(new Element('|'), new Element('_'),new Element('|'))),
			new Digit(
				new Line(new Element(' '), new Element('_'),new Element(' ')),
				new Line(new Element('|'), new Element('_'),new Element('|')),
				new Line(new Element(' '), new Element('_'),new Element('|'))),
		};

		public Digit(Line top, Line middle, Line bottom)
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
