using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckoutKata
{
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
        
        private static double Discount(IEnumerable<Item> items)
        {
            var offers = new List<Offer>
            {
                new Offer("A", 3, 20),
                new Offer("B", 2, 15)
            };

            return offers.Sum(offer => CalculateDiscount(items, offer));
        }

        private static double CalculateDiscount(IEnumerable<Item> items, Offer offer)
        {
            double numberOfItems = items.Count(item => item.Sku == offer.Sku);
            return Math.Floor(numberOfItems / offer.QualifyingAmount) * offer.Discount;
        }
    }
}
