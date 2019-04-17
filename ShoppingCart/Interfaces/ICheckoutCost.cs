using System;
using System.Collections.Generic;
using System.Text;
using ShoppingCart.Offers;

namespace ShoppingCart.Interfaces
{
    public interface ICheckoutCost
    {
        decimal GetTotalCost();
        decimal GetTotalCost(IList<KeyValuePair<int, OfferFlags>> lstProductOfferId);
    }
}
