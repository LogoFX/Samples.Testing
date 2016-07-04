using System;
using FakeItEasy;
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
    }
}