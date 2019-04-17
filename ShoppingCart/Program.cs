using System;
using System.Collections.Generic;
using ShoppingCart.Models;
using ShoppingCart.Interfaces;
using ShoppingCart.Checkout;
using ShoppingCart.Checkout.Process;

namespace ShoppingCart
{
    class Program
    {
        static void Main(string[] args)
        {
            //v1. check out for a shopping cart and get its cost
            ShoppingCart();

        }

        private static void ShoppingCart()
        {
            IList<ICheckoutItem> _lstCheckItem = new List<ICheckoutItem>();
            IList<IProduct> _lstProduct = new List<IProduct>();
            //set up store products
            _lstProduct = SetupProducts();

            //check out items
            var cart = new CheckoutProcesor(_lstCheckItem, _lstProduct);
            var item = new CheckoutItem() { CheckoutItemId = 1, ProductId = 1, Quantity = 1 };
            cart.AddItem(item);

            item = new CheckoutItem() { CheckoutItemId = 2, ProductId = 2, Quantity = 1 };
            cart.AddItem(item);

            //get the cost for the checkout items 
            var tot = cart.GetTotalCost();

            Console.WriteLine($"Total number of the items checked out and the cost: {cart.GetTotalItems().Count}  {tot.ToString("C")}.");

        }



        private static IList<IProduct> SetupProducts()
        {
            IList<IProduct> _lstProduct = new List<IProduct>();
            //set up store products
            var p = new Product() { ProductId = 1, ProductName = "apple", UnitPrice = 0.6M };
            _lstProduct.Add(p);
            p = new Product() { ProductId = 2, ProductName = "orange", UnitPrice = 0.25M };
            _lstProduct.Add(p);

            return _lstProduct;
        }
    }
}
