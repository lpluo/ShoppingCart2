﻿using System;
using System.Collections.Generic;
using System.Text;
using ShoppingCart.Interfaces;
using ShoppingCart.Models;
using System.Linq;
using ShoppingCart.Offers;

namespace ShoppingCart.Checkout.Process
{
    public class CheckoutProcesor : ICheckoutProcesor, ICheckoutCost
    {
        IList<ICheckoutItem> _lstCheckoutItem;
        IList<IProduct> _lstProduct;
        public CheckoutProcesor(IList<ICheckoutItem> lstCheckoutItem, IList<IProduct> lstProduct)
        {
            this._lstCheckoutItem = lstCheckoutItem;
            this._lstProduct = lstProduct;
        }
        public void AddItem(ICheckoutItem item)
        {
            _lstCheckoutItem.Add(item);
        }
        public void RemoveItem(ICheckoutItem item)
        {
            _lstCheckoutItem.Remove(item);
        }
        public IList<ICheckoutItem> GetTotalItems()
        {
            return _lstCheckoutItem;
        }


        /// <summary>
        /// Get total cost for the current checkout items without offer
        /// </summary>
        /// <returns>Cost</returns>
        public decimal GetTotalCost()
        {

            var qtyItems = from item in _lstCheckoutItem
                           join product in _lstProduct
                           on item.ProductId equals product.ProductId
                           select new
                           { item.Quantity, product.UnitPrice };
            var tot = qtyItems.Sum(item => item.Quantity * item.UnitPrice);
            return tot;

        }

        /// <summary>
        /// Get total cost for the current checkout items with a list of offers
        /// </summary>
        /// <param name="lstProductOffer">A list of offers</param>
        /// <returns>Cost</returns>
        public decimal GetTotalCost(IList<KeyValuePair<int, OfferFlags>> lstProductOffer)
        {
            var discount = GetOfferDiscount(lstProductOffer);
            var tot = GetTotalCost() - discount;
            return tot;

        }
        public decimal GetOfferDiscount(IList<KeyValuePair<int, OfferFlags>> lstProductOffer)
        {
            var offer = new Offers.Offers(_lstCheckoutItem, _lstProduct, lstProductOffer);
            var discount = offer.GetDiscount();
            return discount;

        }

    }
}
