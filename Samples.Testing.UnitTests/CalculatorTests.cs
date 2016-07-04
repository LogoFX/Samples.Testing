using Samples.Testing.Domain;
using Xunit;

namespace Samples.Testing.UnitTests
{
    public class CalculatorTests : TestsBase.WithRootObject<ICalculator, Calculator>
    {                    
        [Fact]
        public void Calculate_CurrencyCodeIsSupported_CalculatedAmountIsCorrect()
        {
            var originalAmount = 5;
            var fromCurrencyCode = 7;
            var toCurrencyCode = 13;
            var conversionRate = 1.5;            
            RegisterBuilder(() => Steps.SetupSuccesfulConversion(fromCurrencyCode, toCurrencyCode,
                conversionRate));
            RegisterBuilder(Steps.SetupLogProvider);            
            
            var calculator = GetRootObject();
            var calculatedAmount = calculator.Calculate(originalAmount, fromCurrencyCode, toCurrencyCode);

            var expectedAmount = 7.5;
            Steps.AssertCalculatedAmount(calculatedAmount, expectedAmount);          
        }

        [Fact]
        public void Calculate_CurrencyCodeIsNotSupported_UnableToCalculate()
        {
            var originalAmount = 5;
            var fromCurrencyCode = 7;
            var toCurrencyCode = 14;            
            RegisterBuilder(() => Steps.SetupFailedConversion(fromCurrencyCode, toCurrencyCode));             
            var mockLogProvider = Steps.SetupLogProvider();
            RegisterInstance(mockLogProvider);

            var calculator = GetRootObject();
            var exception = Record.Exception(() => calculator.Calculate(originalAmount, fromCurrencyCode, toCurrencyCode));

            Steps.AssertExceptionIsThrowForUnsupportedCurrencyCode(exception, 14);
            Steps.VerifyLogMessageIsAdded(mockLogProvider);
        }
    }    
}
