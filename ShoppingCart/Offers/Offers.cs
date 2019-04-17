using System;
using System.Collections.Generic;
using System.Text;
using ShoppingCart.Models;
using ShoppingCart.Interfaces;
using System.Linq;
using ShoppingCart.Offers;

namespace ShoppingCart.Offers
{
    class Offers
    {
        IList<ICheckoutItem> _lstCheckoutItem;
        IList<IProduct> _lstProduct;
        IList<KeyValuePair<int,OfferFlags  > >_lstProductOffer;
   
        public Offers(IList<ICheckoutItem> lstCheckoutItem, IList<IProduct> lstProduct, IList<KeyValuePair<int, OfferFlags> > lstProductOffer)
        {
            this._lstCheckoutItem = lstCheckoutItem;
            this._lstProduct = lstProduct;
            this._lstProductOffer = lstProductOffer;
        }
        
        /// <summary>
        /// Get discount for all offers    
        /// </summary>
        /// <returns>Discount</returns>
        public decimal GetDiscount()
        {           
            decimal discount = 0M;
            discount =  GetDiscount(_lstProductOffer);

            return discount;
        }

        /// <summary>
        /// Get discount for the given offers   
        /// </summary>
        /// <param name="lstProductOffer"> a list of offers</param>
        /// <returns>Discount</returns>
        public decimal GetDiscount(IList<KeyValuePair<int, OfferFlags>> lstProductOffer)
        {            
            this._lstProductOffer = lstProductOffer;
            decimal discount = 0M;
            if (lstProductOffer != null && lstProductOffer.Count() > 0)
            {
                foreach (var item in lstProductOffer)
                {
                    discount = discount + GetDiscount(item);
                }
            }

            return discount;
        }
        /// <summary>
        /// Get discount for the given offer  
        /// </summary>
        /// <param name="productOffer">given offer</param>
        /// <returns>Discount</returns>
        public decimal GetDiscount(KeyValuePair<int, OfferFlags> productOffer)
        {
            decimal discount = 0M;
            //find all the possible offers
            var discount1 = GetDiscountBuy1Get1Free(productOffer);
            var discount2= GetDiscount3For2(productOffer);
            //add all tegether
            discount = discount1 + discount2;

            return discount;
        }
      /// <summary>
      /// Calculate discount for buy 1 get free offer
      /// </summary>
      /// <param name="productOffer">A given offer</param>
      /// <returns>Discount</returns>
        private decimal GetDiscountBuy1Get1Free(KeyValuePair<int, OfferFlags> productOffer)
        {
            
            if ( productOffer.Value != OfferFlags.BuyOneGetOneFree)
                return 0M;

            var qtyItems=GetDiscountItems( productOffer.Key);
            int i= 0;
            decimal discount = 0M;
            if (qtyItems.Count() > 0)
            {
                foreach (var item in qtyItems)
                {
                    i = i + 1;
                    if ((i % 2) == 0)
                    {
                        discount = discount + item.Quantity * item.UnitPrice;
                    }

                }
            }

            return discount;
        }
        /// <summary>
        /// Calculate discount for 3 For 2 offer
        /// </summary>
        /// <param name="productOffer">A given offer</param>
        /// <returns>Discount</returns>
        private decimal GetDiscount3For2(KeyValuePair<int, OfferFlags> productOffer)
        {
            if (productOffer.Value != OfferFlags.ThreeForTwo)
                return 0M;

            var qtyItems = GetDiscountItems(productOffer.Key);
            int i = 0;
            decimal discount = 0M;
            if (qtyItems.Count() > 0)
            {
                foreach (var item in qtyItems)
                {
                    i = i + 1;
                    if ((i % 3) == 0)
                    {
                        discount = discount + item.Quantity * item.UnitPrice;
                    }

                }
            }
            return discount;
        }

        /// <summary>
        /// Find the checkout items matched the given offer
        /// </summary>
        /// <param name="productOfferId">A given offerr</param>
        /// <returns>The checkout items matched the given offer</returns>
        public IEnumerable<dynamic> GetDiscountItems(int productOfferId)
        {
            var qtyItems = from item in _lstCheckoutItem
                           join product in _lstProduct
                           on item.ProductId equals product.ProductId
                           where item.ProductId== productOfferId
                           select new
                           { item.CheckoutItemId, item.Quantity, product.UnitPrice };
            return qtyItems;
            //return qtyItems.ToList();
        }
    }
}
