using OnlineShopping.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Services.ItemService
{
    public interface IItemService
    {
        ResultViewModel GetItems();
    }
}
