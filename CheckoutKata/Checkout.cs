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

        public double GetTotal(IEnumerable<Offer> offers)
        {
            var subTotal = _items.Sum(item => item.Price);
            return subTotal - Discount(_items, offers);
        }
        
        private static double Discount(IEnumerable<Item> items, IEnumerable<Offer> offers)
        {
            return offers.Sum(offer => CalculateDiscount(items, offer));
        }

        private static double CalculateDiscount(IEnumerable<Item> items, Offer offer)
        {
            double numberOfItems = items.Count(item => item.Sku == offer.Sku);
            return Math.Floor(numberOfItems / offer.QualifyingAmount) * offer.Discount;
        }
    }
}
