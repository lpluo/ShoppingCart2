using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Interfaces
{
    interface ICheckoutProcesor
    {
        void AddItem(ICheckoutItem item);
        void RemoveItem(ICheckoutItem item);
        IList<ICheckoutItem> GetTotalItems();

    }
}
