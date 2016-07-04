using System;
using FluentAssertions;
using Samples.Testing.Domain;
using Xunit;

namespace Samples.Testing.UnitTests
{
    public class CalculatorTests
    {
        [Fact]
        public void Calculate_CurrencyCodeIsSupported_CalculatedAmountIsCorrect()
        {
            var originalAmount = 5;
            var currencyCode = 7;

            var calculator = new Calculator();
            var calculatedAmount = calculator.Calculate(originalAmount, currencyCode);

            var expectedAmount = 5;
            calculatedAmount.Should().Be(expectedAmount);
        }

        [Fact]
        public void Calculate_CurrencyCodeIsNotSupported_UnableToCalculate()
        {
            var originalAmount = 5;
            var currencyCode = 8;

            var calculator = new Calculator();
            var exception = Record.Exception(() => calculator.Calculate(originalAmount, currencyCode));

            exception.Should().NotBeNull()
                .And.Subject.As<Exception>().Message.Should().Contain("unexpected currency code: 7");
        }
    }
}
