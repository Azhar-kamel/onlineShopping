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
    public class DeleteBll : IDeleteBll
    {
        private readonly IUnitOfWork _UnitOfWork;
        public DeleteBll(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }
        public ResultViewModel DeleteFromCart(DeleteFromCart cartData)
        {
            ResultViewModel result = new ResultViewModel()
            {
                Message = "I can not Delete this item",
                Succes = false
            };

            Customer customer = _UnitOfWork._CustomerRepository.FindBy(c => c.Email == cartData.customerEmail);
            
            if (customer != null)
            {
                var cart = _UnitOfWork._CartRepository
                    .FindBy(c => c.CustomerId == customer.Id && c.Status == Status.open);
                //I have two optione if it is the last item in cart i should
                //delete cart and item else delete item only

                if (cart!=null)
                {
                    var cartItems = _UnitOfWork._CartItemRepository
                        .Find(a => a.CartId == cart.Id)
                        .ToList();
                    foreach (var item in cartItems)
                    {
                        if (item.ItemId == cartData.itemId)
                        {
                            _UnitOfWork._CartItemRepository.Delete(item);
                            break;
                        }
                    }
                    if (cartItems.Count()==1)
                    {
                        _UnitOfWork._CartRepository.Delete(cart);
                    }
                    _UnitOfWork.SaveChanges();
                    result.Message = "Deleted Successfuly";
                    result.Succes = true;
                }
            }

            return result;
        }
    }
}
