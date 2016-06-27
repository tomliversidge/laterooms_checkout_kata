namespace CheckoutKata
{
    public class Offer
    {
        public string Sku { get; }
        public int QualifyingAmount { get; }
        public double Discount { get; }

        public Offer(string sku, int qualifyingAmount, double discount)
        {
            Sku = sku;
            QualifyingAmount = qualifyingAmount;
            Discount = discount;
        }
    }
}