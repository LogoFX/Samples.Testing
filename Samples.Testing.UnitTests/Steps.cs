using System;
using FakeItEasy;
using FluentAssertions;
using Samples.Testing.Domain;

namespace Samples.Testing.UnitTests
{
    public static class Steps
    {
        internal static ICurrencyRatesProvider SetupSuccesfulConversion(int fromCurrencyCode, int toCurrencyCode, double conversionRate)
        {
            var stubCurrencyRatesProvider = A.Fake<ICurrencyRatesProvider>();
            A.CallTo(() => stubCurrencyRatesProvider.GetRate(fromCurrencyCode, toCurrencyCode)).Returns(conversionRate);
            return stubCurrencyRatesProvider;
        }

        internal static ICurrencyRatesProvider SetupFailedConversion(int fromCurrencyCode, int toCurrencyCode)
        {
            var stubCurrencyRatesProvider = A.Fake<ICurrencyRatesProvider>();
            A.CallTo(() => stubCurrencyRatesProvider.GetRate(fromCurrencyCode, toCurrencyCode)).Throws<Exception>();
            return stubCurrencyRatesProvider;
        }

        internal static ILogProvider SetupLogProvider()
        {
            return A.Fake<ILogProvider>();
        }

        internal static void AssertCalculatedAmount(double actualAmount, double expectedAmount)
        {
            actualAmount.Should().Be(expectedAmount);
        }

        internal static void AssertExceptionIsThrowForUnsupportedCurrencyCode(Exception exception, int currencyCode)
        {
            exception.Should().NotBeNull()
                .And.Subject.As<Exception>().Message.Should().Contain($"Currency code {currencyCode} is not supported");
        }

        internal static void VerifyLogMessageIsAdded(ILogProvider logProvider)
        {
            A.CallTo(() => logProvider.Log("Fatal exception has happened")).MustHaveHappened(Repeated.Exactly.Once);
        }
    }
}