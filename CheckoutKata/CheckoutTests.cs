using System.Collections.Generic;
using Xunit;

namespace CheckoutKata
{
    public class CheckoutTests
    {
        private readonly Item _item1 = new Item("A", 50);
        private readonly Item _item2 = new Item("B", 30);
        private readonly Item _item3 = new Item("C", 20);
        private readonly Item _item4 = new Item("D", 15);

        private readonly List<Offer> _offers = new List<Offer>
        {
            new Offer("A", 3, 20),
            new Offer("B", 2, 15)
        };

        [Fact]
        public void can_scan_a_single_item_and_get_total()
        {
            var checkout = new Checkout();
            checkout.Scan(_item1);
            Assert.Equal(50, checkout.GetTotal(_offers));
        }

        [Fact]
        public void can_scan_two_items_and_get_total()
        {
            var checkout = new Checkout();
            checkout.Scan(_item1);
            checkout.Scan(_item2);
            Assert.Equal(80, checkout.GetTotal(_offers));
        }

        [Fact]
        public void can_apply_one_offer_at_checkout()
        {
            var checkout = new Checkout();
            ScanItemMultipleTimes(checkout, _item1, 3); // total = 150, discount = 20
            Assert.Equal(130, checkout.GetTotal(_offers));
        }

        [Fact]
        public void can_apply_two_offers_at_checkout()
        {
            var checkout = new Checkout();
            ScanItemMultipleTimes(checkout, _item1, 3); // total = 150, discount = 20
            ScanItemMultipleTimes(checkout, _item2, 2); // total = 60, discount = 15
            var expectedSubTotal = 210;
            var expectedDiscount = 35;
            var expectedTotal = expectedSubTotal - expectedDiscount;
            Assert.Equal(expectedTotal, checkout.GetTotal(_offers));
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
            Assert.Equal(expectedTotal, checkout.GetTotal(_offers));
        }

        [Fact]
        public void can_scan_items_with_and_without_offers_at_checkout()
        {
            var checkout = new Checkout();
            ScanItemMultipleTimes(checkout, _item1, 6); // total = 300, discount = 40
            ScanItemMultipleTimes(checkout, _item2, 4); // total = 120, discount = 30
            checkout.Scan(_item3); // total = 20, no discount
            checkout.Scan(_item4); // total = 15, no discount
            var expectedSubTotal = 455;
            var expectedDiscount = 70;
            var expectedTotal = expectedSubTotal - expectedDiscount;
            Assert.Equal(expectedTotal, checkout.GetTotal(_offers));
        }

        [Fact]
        public void can_apply_different_offers()
        {
            var checkout = new Checkout();

            var offers = new List<Offer>
            {
                new Offer("A", 2, 20),
                new Offer("J", 1, 10)
            };

            ScanItemMultipleTimes(checkout, _item1, 6); // total = 300, discount = 60
            ScanItemMultipleTimes(checkout, _item2, 4); // total = 120, discount = 0
           
            var expectedSubTotal = 420;
            var expectedDiscount = 60;
            var expectedTotal = expectedSubTotal - expectedDiscount;
            Assert.Equal(expectedTotal, checkout.GetTotal(offers));
        }

        private void ScanItemMultipleTimes(Checkout checkout, Item item, int numberOfTimesToScan)
        {
            for (int i = 0; i < numberOfTimesToScan; i++)
            {
                checkout.Scan(item);
            }
        }
    }
}