using OnlineShopping.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Services.CartService
{
    public interface ICartService
    {
        ResultViewModel AddToCart(AddCartViewModel addCart);
        ResultViewModel DeleteItemFromCart(DeleteFromCart deleteItem);
        ResultViewModel CheckoutCart(string CustomerEmail);
        ResultViewModel GetCustomerCart(string CustomerEmail);
        ResultViewModel GetOrderList(string CustomerEmail);
    }
}
