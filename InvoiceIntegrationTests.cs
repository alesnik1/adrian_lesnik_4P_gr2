using Xunit;
using SuperProstyFaktury;
using System.Linq;

namespace SuperProstyFaktury.Tests
{
    public class InvoiceIntegrationTests
    {
        // === 4 PROSTE TESTY INTEGRACYJNE (Integration Tests) ===
        // Każdy test łączy ze sobą Service, Calculator oraz Database.

        [Fact]
        public void Integration_ShouldSaveInvoiceWithStandardVat()
        {
            // Arrange: Łączymy wszystkie 3 komponenty w jeden system
            var db = new InvoiceDatabase();
            var calculator = new TaxCalculator();
            var service = new InvoiceService(db, calculator);

            // Act
            service.CreateInvoice("Firma ABC", 1000, 23);

            // Assert
            var saved = db.SavedInvoices.FirstOrDefault(i => i.Customer == "Firma ABC");

            Assert.NotNull(saved);
            Assert.Equal(1230m, saved.GrossPrice); // 1000 + 23%
        }

        [Fact]
        public void Integration_ShouldSaveInvoiceWithReducedVat()
        {
            // Arrange
            var db = new InvoiceDatabase();
            var calculator = new TaxCalculator();
            var service = new InvoiceService(db, calculator);

            // Act: Obniżony VAT 8%
            service.CreateInvoice("Księgarnia", 500, 8);

            // Assert
            var saved = db.SavedInvoices.FirstOrDefault(i => i.Customer == "Księgarnia");

            Assert.NotNull(saved);
            Assert.Equal(540m, saved.GrossPrice); // 500 + 8%
        }

        [Fact]
        public void Integration_ShouldNotSaveAnything_WhenVatIsInvalid()
        {
            // Arrange
            var db = new InvoiceDatabase();
            var calculator = new TaxCalculator();
            var service = new InvoiceService(db, calculator);

            // Act: Ujemny VAT powinien przerwać cały proces
            Assert.Throws<ArgumentException>(() => service.CreateInvoice("Błąd Sp. z o.o.", 100, -10));

            // Assert: Baza ma pozostać pusta
            Assert.Empty(db.SavedInvoices);
        }

        [Fact]
        public void Integration_ShouldCorrectlyAccumulateMultipleInvoicesInDatabase()
        {
            // Arrange
            var db = new InvoiceDatabase();
            var calculator = new TaxCalculator();
            var service = new InvoiceService(db, calculator);

            // Act: Dwie faktury w tej samej bazie
            service.CreateInvoice("Klient A", 100, 23);
            service.CreateInvoice("Klient B", 200, 0);

            // Assert
            Assert.Equal(2, db.SavedInvoices.Count);

            var first = db.SavedInvoices.FirstOrDefault(i => i.Customer == "Klient A");
            var second = db.SavedInvoices.FirstOrDefault(i => i.Customer == "Klient B");

            Assert.Equal(123m, first?.GrossPrice);
            Assert.Equal(200m, second?.GrossPrice);
        }
    }
}