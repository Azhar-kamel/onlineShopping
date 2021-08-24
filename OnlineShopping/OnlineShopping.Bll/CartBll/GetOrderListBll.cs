using Microsoft.EntityFrameworkCore;
using OnlineShopping.Data.UnitOfWork;
using OnlineShopping.DTO;
using OnlineShopping.Entities;
using OnlineShopping.Shared;
using OnlineShopping.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Bll.CartBll
{
    public class GetOrderListBll : IGetOrderListBll
    {
        private readonly IUnitOfWork _UnitOfWork;
        public GetOrderListBll(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }
        public ResultViewModel GetOrderList(Customer customer)
        {
            ResultViewModel result = new ResultViewModel()
            {
                Message = "There is not list",
                Succes = false
            };
            List<OrderListDTO> carts = _UnitOfWork._CartRepository
                .GetAllAsQueryable()
                .Where(c => c.CustomerId == customer.Id && c.Status == Status.close)
                .Include(a=>a.cartItems)
                .Select(b=>new OrderListDTO(){ 
                    CartId=b.Id,
                    RecievedData=b.RecievedData,
                    OrderData=b.OrderData,
                    RequestData=b.RequestData
                }).ToList();
            if (carts.Count()>0)
            {
                foreach(var item in carts)
                {
                    var cartItems=_UnitOfWork._CartItemRepository
                        .GetAllAsQueryable()
                        .Where(c => c.CartId == item.CartId)
                        .Include(a => a.Item)
                        .ThenInclude(b => b.Discount)
                        .Include(a => a.Item)
                        .ThenInclude(b => b.Tax)
                        .Select(c => new ViewCustomerCartDTO
                        {
                            itemId = c.ItemId,
                            itemImage = c.Item.ImageId,
                            itemName = c.Item.Name,
                            itemPrice = c.Item.price,
                            itemDiscount = c.Item.Discount == null ? 0 : c.Item.Discount.value,
                            itemQuantity = c.Quantity,
                            itemTax = c.Item.Tax.value
                        })
                        .ToList();
                    StringBuilder ItemsName = new StringBuilder();
                    foreach (var items in cartItems)
                    {
                        items.itemTotalPrice = GetTotalPrice(items.itemQuantity, items.itemPrice, items.itemDiscount, items.itemTax);
                        ItemsName.Append($"{items.itemName} ");
                    }
                    item.ItemsName=ItemsName.ToString();
                    item.cartDTOs.AddRange(cartItems);
                }
                result.Message = "There is a list";
                result.Succes = true;
                result.ReturnObject = carts;
            }
            return result;
        }
        private decimal GetTotalPrice(int quantity, decimal Price, decimal Discount, decimal Tax)
        {
            decimal totalPrice = quantity * Price;
            if (Discount > 0)
            {
                totalPrice = totalPrice - (totalPrice * (Discount / 100));
                totalPrice = totalPrice + (totalPrice * (Tax / 100));
            }
            else
            {
                totalPrice = totalPrice + (totalPrice * (Tax / 100));
            }
            return totalPrice;
        }
    }
}
