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
        private readonly ILogProvider _logProvider;

        public Calculator(ICurrencyRatesProvider currencyRatesProvider, ILogProvider logProvider)
        {
            _currencyRatesProvider = currencyRatesProvider;
            _logProvider = logProvider;
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
                _logProvider.Log("Fatal exception has happened");
                throw new InvalidOperationException($"Currency code {toCurrencyCode} is not supported");
            }                       
        }
    }    
}
