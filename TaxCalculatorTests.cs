
using Xunit;
using SuperProstyFaktury;

namespace SuperProstyFaktury.Tests
{
    public class TaxCalculatorTests
    {
        // === 4 PROSTE TESTY JEDNOSTKOWE (Unit Tests) ===
        // Testują tylko jedną odizolowaną metodę: TaxCalculator.AddVat

        [Fact]
        public void AddVat_ShouldAdd23Percent_WhenStandardVat()
        {
            // Arrange
            var calculator = new TaxCalculator();

            // Act
            var result = calculator.AddVat(100, 23);

            // Assert
            Assert.Equal(123m, result);
        }

        [Fact]
        public void AddVat_ShouldReturnNetPrice_WhenVatIsZero()
        {
            // Arrange
            var calculator = new TaxCalculator();

            // Act
            var result = calculator.AddVat(200, 0);

            // Assert
            Assert.Equal(200m, result);
        }

        [Fact]
        public void AddVat_ShouldReturnZero_WhenNetPriceIsZero()
        {
            // Arrange
            var calculator = new TaxCalculator();

            // Act
            var result = calculator.AddVat(0, 23);

            // Assert
            Assert.Equal(0m, result);
        }

        [Fact]
        public void AddVat_ShouldThrowException_WhenVatIsNegative()
        {
            // Arrange
            var calculator = new TaxCalculator();

            // Act + Assert
            Assert.Throws<ArgumentException>(() => calculator.AddVat(100, -5));
        }
    }
}