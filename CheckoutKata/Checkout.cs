using Xunit;

namespace CheckoutKata
{
    public class Item
    {
        public string Sku { get; }
        public double Price { get; }

        public Item(string sku, double price)
        {
            Sku = sku;
            Price = price;
        }
    }

    public class CheckoutTests
    {
        [Fact]
        public void can_scan_a_single_item_and_get_total()
        {
            var item = new Item("A", 50);
            var checkout = new Checkout();
            checkout.Scan(item);
            Assert.Equal(50, checkout.GetTotal());
        }
    }

    public class Checkout
    {
        public void Scan(Item item)
        {
        }

        public int GetTotal()
        {
            return 50;
        }
    }
}
