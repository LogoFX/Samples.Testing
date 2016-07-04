using System;

namespace Samples.Testing.Domain
{
    public interface ICalculator
    {
        double Calculate(double amount, int fromCurrencyCode, int toCurrencyCode);
    }

    public class Calculator : ICalculator
    {
        private readonly ICurrencyRatesProvider _currencyRatesProvider;

        public Calculator(ICurrencyRatesProvider currencyRatesProvider)
        {
            _currencyRatesProvider = currencyRatesProvider;
        }

        public double Calculate(double amount, int fromCurrencyCode, int toCurrencyCode)
        {
            try
            {
                var rate = _currencyRatesProvider.GetRate(fromCurrencyCode, toCurrencyCode);
                return amount*rate;
            }
            catch (Exception)
            {
                throw new InvalidOperationException($"Currency code {toCurrencyCode} is not supported");
            }                       
        }
    }    
}
