using WebAPI.Utilities;
using Xunit;

namespace TestProject
{
	public class UnitTests
	{
		[Theory]
		[InlineData(2499, "$24.99", "en-US")]
		[InlineData(0, "$0.00", "en-US")]
		[InlineData(-60, "-$0.60", "en-US")]
		[InlineData(6000, "￥6,000", "ja-JP")]
		[InlineData(29900, "د.ت.‏ 29,900", "ar-TN")]
		public void testFormatPrice(int price, string formattedPrice, string cultureCode)
		{
			// Arrange

			// Act
			var result = Currency.Format(price, cultureCode);

			// Assert
			Assert.Equal(formattedPrice, result);
		}

		[Theory]
		[InlineData("$24.99", 2499, "en-US")]
		[InlineData("$0.00", 0, "en-US")]
		[InlineData("-$0.60", -60, "en-US")]
		[InlineData("￥6,000", 6000, "ja-JP")]
		[InlineData("د.ت.‏ 29,900", 29900, "ar-TN")]

		public void testParsePrice(string formattedPrice, int price, string cultureCode)
		{
			// Arrange

			// Act
			var result = Currency.Parse(formattedPrice, cultureCode);

			// Assert
			Assert.Equal(price, result);
		}
	}
}
