using System;
using System.Collections.Generic;
using System.Text;
using ShoppingCart.Interfaces;

namespace ShoppingCart.Models
{
    public class CheckoutItem : ICheckoutItem
    {
        public int CheckoutItemId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
