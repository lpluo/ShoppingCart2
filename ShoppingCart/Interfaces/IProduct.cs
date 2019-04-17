using System;
using System.Collections.Generic;
using System.Text;
using ShoppingCart.Interfaces;

namespace ShoppingCart.Interfaces
{
    public interface IProduct
    {
        int ProductId { get; set; }
        string ProductName { get; set; }
        decimal UnitPrice { get; set; }

    }
}
