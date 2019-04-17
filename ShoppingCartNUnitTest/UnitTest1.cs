using NUnit.Framework;
using System.Collections.Generic;
using ShoppingCart.Models;
using ShoppingCart.Interfaces;
using ShoppingCart.Checkout;
using ShoppingCart.Checkout.Process;
using ShoppingCart.Offers;

namespace Tests
{
    public class Tests
    {
        IList<ICheckoutItem> _lstCheckItem = new List<ICheckoutItem>();
        IList<IProduct> _lstProduct = new List<IProduct>();
        [SetUp]
        public void Setup()
        {//set up products
            var p = new Product() { ProductId = 1, ProductName = "apple", UnitPrice = 0.6M };
            _lstProduct.Add(p);
            p = new Product() { ProductId = 2, ProductName = "orange", UnitPrice = 0.25M };
            _lstProduct.Add(p);
        }

        [Test]
        public void Get_Checkout_Total_Cost()
        {//check the cost for the checkout items 
            var cart = new CheckoutProcesor(_lstCheckItem, _lstProduct);
            var item = new CheckoutItem() { CheckoutItemId = 1, ProductId = 1, Quantity = 1 };
            cart.AddItem(item);
            item = new CheckoutItem() { CheckoutItemId = 2, ProductId = 2, Quantity = 2 };
            cart.AddItem(item);
            var tot = cart.GetTotalCost();
            Assert.AreEqual(1.1M, tot);
        }

        [Test]
        public void Get_Checkout_Total_Cost_WithDiscount()
        {//check the cost for the checkout items 
            //some of the items with offer
            IList<KeyValuePair<int, OfferFlags>> lstProductOffer;
            lstProductOffer = new List<KeyValuePair<int, OfferFlags>>()
                { new KeyValuePair<int, OfferFlags>(1, OfferFlags.BuyOneGetOneFree),
                new KeyValuePair<int, OfferFlags>(2, OfferFlags.ThreeForTwo) };

            var cart = new CheckoutProcesor(_lstCheckItem, _lstProduct);
            var item = new CheckoutItem() { CheckoutItemId = 1, ProductId = 1, Quantity = 1 };
            cart.AddItem(item);
            item = new CheckoutItem() { CheckoutItemId = 2, ProductId = 2, Quantity = 2 };
            cart.AddItem(item);
            item = new CheckoutItem() { CheckoutItemId = 3, ProductId = 1, Quantity = 1 };
            cart.AddItem(item);
            var tot = cart.GetTotalCost(lstProductOffer);
            Assert.AreEqual(1.1M, tot);

        }

    }
}