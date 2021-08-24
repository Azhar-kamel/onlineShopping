using OnlineShopping.Entities;
using OnlineShopping.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Bll.CartBll
{
    public interface IGetOrderListBll
    {
        ResultViewModel GetOrderList(Customer Customer);
    }
}
