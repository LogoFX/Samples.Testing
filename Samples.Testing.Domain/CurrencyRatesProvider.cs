using System;

namespace Samples.Testing.Domain
{
    public interface ICurrencyRatesProvider
    {
        double GetRate(int fromCurrencyCode, int toCurrencyCode);
    }

    public class CurrencyRatesProvider : ICurrencyRatesProvider
    {
        public double GetRate(int fromCurrencyCode, int toCurrencyCode)
        {
            throw new NotImplementedException();
        }
    }
}