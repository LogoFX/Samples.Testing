using System;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Practices.Unity;
using Samples.Testing.Domain;
using Xunit;

namespace Samples.Testing.UnitTests
{
    public class CalculatorTests : TestsBase.WithRootObject<ICalculator>
    {            
        public CalculatorTests()
        {                
            Container.RegisterType<ICalculator, Calculator>();
        }

        [Fact]
        public void Calculate_CurrencyCodeIsSupported_CalculatedAmountIsCorrect()
        {
            var originalAmount = 5;
            var fromCurrencyCode = 7;
            var toCurrencyCode = 13;
            var conversionRate = 1.5;            
            var stubCurrencyRatesProvider = A.Fake<ICurrencyRatesProvider>();
            A.CallTo(() => stubCurrencyRatesProvider.GetRate(fromCurrencyCode, toCurrencyCode)).Returns(conversionRate);
            RegisterInstance(stubCurrencyRatesProvider);
            var dummyLogProvider = A.Fake<ILogProvider>();
            RegisterInstance(dummyLogProvider);            
            
            var calculator = GetRootObject();
            var calculatedAmount = calculator.Calculate(originalAmount, fromCurrencyCode, toCurrencyCode);

            var expectedAmount = 7.5;
            calculatedAmount.Should().Be(expectedAmount);            
        }

        [Fact]
        public void Calculate_CurrencyCodeIsNotSupported_UnableToCalculate()
        {
            var originalAmount = 5;
            var fromCurrencyCode = 7;
            var toCurrencyCode = 14;            
            var stubCurrencyRatesProvider = A.Fake<ICurrencyRatesProvider>();
            A.CallTo(() => stubCurrencyRatesProvider.GetRate(fromCurrencyCode, toCurrencyCode)).Throws<Exception>();
            RegisterInstance(stubCurrencyRatesProvider);            
            var mockLogProvider = A.Fake<ILogProvider>();
            RegisterInstance(mockLogProvider);

            var calculator = GetRootObject();
            var exception = Record.Exception(() => calculator.Calculate(originalAmount, fromCurrencyCode, toCurrencyCode));

            exception.Should().NotBeNull()
                .And.Subject.As<Exception>().Message.Should().Contain("Currency code 14 is not supported");
            A.CallTo(() => mockLogProvider.Log("Fatal exception has happened")).MustHaveHappened(Repeated.Exactly.Once);            
        }        
    }    
}
