using System;
using System.Collections.Generic;
using System.Text;
using ShoppingCart.Interfaces;

namespace ShoppingCart.Models
{
    public class Product : IProduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
