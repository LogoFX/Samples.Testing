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
            return 5;
        }
    }
}
