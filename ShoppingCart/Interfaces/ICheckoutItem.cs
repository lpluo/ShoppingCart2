using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Interfaces
{
    public interface ICheckoutItem
    {
        int CheckoutItemId { get; set; }
        int ProductId { get; set; }
        int Quantity { get; set; }
    }
}
