using Microsoft.EntityFrameworkCore;
using OnlineShopping.Data.UnitOfWork;
using OnlineShopping.DTO;
using OnlineShopping.Entities;
using OnlineShopping.Shared;
using OnlineShopping.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Bll.CartBll
{
    public class GetBll : IGetBll
    {
        private readonly IUnitOfWork _UnitOfWork;
        public GetBll(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }
        public ResultViewModel GetCart(string CustomerEmail)
        {
            ResultViewModel result = new ResultViewModel()
            {
                Message = "There is no items in your cart",
                Succes = false
            };

            Customer customer = _UnitOfWork._CustomerRepository.FindBy(c => c.Email == CustomerEmail);
            if (customer != null)
            {
                Cart cart = _UnitOfWork._CartRepository.FindBy(c => c.CustomerId == customer.Id && c.Status == Status.open);
                if (cart != null)
                {
                    var CartItems = _UnitOfWork._CartItemRepository
                        .GetAllAsQueryable()
                        .Where(c=>c.CartId== cart.Id)
                        .Include(a => a.Item)
                        .ThenInclude(b=>b.Discount)
                        .Include(a=>a.Item)
                        .ThenInclude(b=>b.Tax)
                        .Select(c=>new ViewCustomerCartDTO 
                        { 
                            itemId=c.ItemId,
                            itemImage=c.Item.ImageId,
                            itemName=c.Item.Name,
                            itemPrice=c.Item.price,
                            itemDiscount=c.Item.Discount==null?0: c.Item.Discount.value,
                            itemQuantity=c.Quantity,
                            itemTax=c.Item.Tax.value
                        })
                        .ToList();
                    foreach(var item in CartItems)
                    {
                        item.itemTotalPrice = GetTotalPrice(item.itemQuantity,item.itemPrice, item.itemDiscount, item.itemTax);
                    }
                    result.Message = "There is items in your cart";
                    result.Succes = true;
                    result.ReturnObject = CartItems;
                }
            }
            return result;
        }

        private decimal GetTotalPrice(int quantity, decimal Price,decimal Discount,decimal Tax)
        {
            decimal totalPrice = quantity* Price;
            if(Discount>0)
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
