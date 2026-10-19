namespace SuperProstyFaktury
{
    public class Invoice
    {
        public string Customer { get; set; } = "";
        public decimal GrossPrice { get; set; }
    }

    public class TaxCalculator
    {
        public decimal AddVat(decimal netPrice, decimal vatPercent)
        {
            if (vatPercent < 0)
                throw new ArgumentException("VAT nie może być ujemny");

            return netPrice + netPrice * vatPercent / 100;
        }
    }

    public class InvoiceDatabase
    {
        public List<Invoice> SavedInvoices { get; } = new();

        public void Save(Invoice invoice) => SavedInvoices.Add(invoice);
    }

    public class InvoiceService
    {
        private readonly InvoiceDatabase _db;
        private readonly TaxCalculator _calculator;

        public InvoiceService(InvoiceDatabase db, TaxCalculator calculator)
        {
            _db = db;
            _calculator = calculator;
        }

        public void CreateInvoice(string customer, decimal netPrice, decimal vatPercent)
        {
            var gross = _calculator.AddVat(netPrice, vatPercent);
            _db.Save(new Invoice { Customer = customer, GrossPrice = gross });
        }
    }
}