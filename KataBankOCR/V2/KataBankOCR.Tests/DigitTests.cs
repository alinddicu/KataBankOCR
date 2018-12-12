namespace KataBankOCR.Tests
{
	using System;
	using System.IO;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using NFluent;

	using Digit;

	[TestClass]
	public class DigitTests
	{
		[TestMethod]
		[DeploymentItem("Digits.txt")]
		public void CheckAllDigits()
		{
			var expected = File.ReadAllText("Digits.txt");
			var checkedString = string.Join<Digit>(Environment.NewLine + Environment.NewLine, Digit.Digits);
			Check.That(checkedString).IsEqualTo(expected);
		}
	}
}
