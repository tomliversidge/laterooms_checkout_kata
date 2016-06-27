using System.Collections.Generic;
using System.Linq;
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

        [Fact]
        public void can_scan_two_items_and_get_total()
        {
            var item1 = new Item("A", 50);
            var item2 = new Item("B", 30);
            var checkout = new Checkout();
            checkout.Scan(item1);
            checkout.Scan(item2);
            Assert.Equal(80, checkout.GetTotal());
        }
    }

    public class Checkout
    {
        private readonly List<Item> _items = new List<Item>(); 

        public void Scan(Item item)
        {
            _items.Add(item);
        }

        public double GetTotal()
        {
            return _items.Sum(item => item.Price);
        }
    }
}
