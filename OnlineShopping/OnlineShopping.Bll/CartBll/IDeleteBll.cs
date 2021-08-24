using OnlineShopping.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Bll.CartBll
{
    public interface IDeleteBll
    {
        ResultViewModel DeleteFromCart(DeleteFromCart cartData);
    }
}
