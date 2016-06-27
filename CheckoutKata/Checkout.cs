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
        private Item _item1 = new Item("A", 50);
        private Item _item2 = new Item("B", 30);
        
        [Fact]
        public void can_scan_a_single_item_and_get_total()
        {
            var checkout = new Checkout();
            checkout.Scan(_item1);
            Assert.Equal(50, checkout.GetTotal());
        }

        [Fact]
        public void can_scan_two_items_and_get_total()
        {
            var checkout = new Checkout();
            checkout.Scan(_item1);
            checkout.Scan(_item2);
            Assert.Equal(80, checkout.GetTotal());
        }

        [Fact]
        public void can_apply_offers_at_checkout()
        {
            var checkout = new Checkout();    
            checkout.Scan(_item1);
            checkout.Scan(_item1);
            checkout.Scan(_item1);
            Assert.Equal(130, checkout.GetTotal());
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
            var subTotal = _items.Sum(item => item.Price);
            return subTotal - Discount(_items);
        }

        private double Discount(IEnumerable<Item> items)
        {
            return items.Count(item => item.Sku == "A") == 3 ? 20 : 0;
        }
    }
}
