using System;

namespace Samples.Testing.Domain
{
    public interface ICalculator
    {
        double Calculate(double amount, int currencyCode);
    }

    public class Calculator : ICalculator
    {
        public double Calculate(double amount, int currencyCode)
        {
            if (currencyCode == 7)
            {
                return amount;
            }
            throw new InvalidOperationException($"Currency code {currencyCode} is not supported");
            
        }
    }
}
