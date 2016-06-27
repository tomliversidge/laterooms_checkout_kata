using System;
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

    public class CheckoutTests
    {
        private readonly Item _item1 = new Item("A", 50);
        private readonly Item _item2 = new Item("B", 30);
        
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
        public void can_apply_one_offer_at_checkout()
        {
            var checkout = new Checkout();
            ScanItemMultipleTimes(checkout, _item1, 3);
            Assert.Equal(130, checkout.GetTotal());
        }

        [Fact]
        public void can_apply_two_offers_at_checkout()
        {
            var checkout = new Checkout();
            ScanItemMultipleTimes(checkout, _item1, 3);
            ScanItemMultipleTimes(checkout, _item2, 2);
            Assert.Equal(175, checkout.GetTotal());
        }

        [Fact]
        public void can_apply_multiple_offers_multiple_times_at_checkout()
        {
            var checkout = new Checkout();
            ScanItemMultipleTimes(checkout, _item1, 6); // total = 300, discount = 40
            ScanItemMultipleTimes(checkout, _item2, 4); // total = 120, discount = 30
            var expectedSubTotal = 420;
            var expectedDiscount = 70;
            var expectedTotal = expectedSubTotal - expectedDiscount;
            Assert.Equal(expectedTotal, checkout.GetTotal());
        }

        private void ScanItemMultipleTimes(Checkout checkout, Item item, int numberOfTimesToScan)
        {
            for (int i = 0; i < numberOfTimesToScan; i++)
            {
                checkout.Scan(item);
            }
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
            var offers = new List<Offer>
            {
                new Offer("A", 3, 20),
                new Offer("B", 2, 15)
            };

            double discount = 0;
            foreach (var offer in offers)
            {
                discount += CalculateDiscount(items, offer);
            }

            return discount;
        }

        private double CalculateDiscount(IEnumerable<Item> items, Offer offer)
        {
            double numberOfItems = items
                .Count(item => item.Sku == offer.Sku);
            return Math.Floor(numberOfItems / offer.QualifyingAmount) * offer.Discount;
        }
    }
}
