using FluentAssertions;
using Samples.Testing.Domain;
using Xunit;

namespace Samples.Testing.UnitTests
{
    public class CalculatorTests
    {
        [Fact]
        public void Calculate_OriginalAmountIsCorrect_CalculatedAmountIsCorrect()
        {
            var originalAmount = 5;
            var currencyCode = 7;

            var calculator = new Calculator();
            var calculatedAmount = calculator.Calculate(originalAmount, currencyCode);

            calculatedAmount.Should().Be(5);
        }
    }
}
