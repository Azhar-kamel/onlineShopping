using OnlineShopping.Bll.ItemBll;
using OnlineShopping.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Services.ItemService
{
    public class ItemService: IItemService
    {
        private readonly IGetItemsBll _GetItemBll;
        public ItemService(IGetItemsBll GetItemBll)
        {
            _GetItemBll = GetItemBll;
        }

        public ResultViewModel GetItems()
        {
            return _GetItemBll.ReturnItems();
        }
    }
}
