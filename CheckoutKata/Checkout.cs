﻿using System.Collections.Generic;
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
            var aDiscount = items.Count(item => item.Sku == "A") == 3 ? 20 : 0;
            var bDiscount = items.Count(item => item.Sku == "B") == 2 ? 15 : 0;
            return aDiscount + bDiscount;
        }
    }
}
