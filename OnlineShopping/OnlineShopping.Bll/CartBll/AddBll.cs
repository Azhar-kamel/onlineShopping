using Microsoft.EntityFrameworkCore;
using OnlineShopping.Data.UnitOfWork;
using OnlineShopping.Entities;
using OnlineShopping.Shared;
using OnlineShopping.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Bll.CartBll
{
    public class AddBll : IAddBll
    {
        private readonly IUnitOfWork _UnitOfWork;
        public AddBll(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }
        public ResultViewModel AddToCart(AddCartViewModel addCart)
        {
            ResultViewModel result = new ResultViewModel()
            {
                Message = "I can not add this item",
                Succes = false
            };
            try
            {
                Customer customer = _UnitOfWork._CustomerRepository.FindBy(c => c.Email == addCart.customerEmail);
                if (customer != null)
                {
                    Cart cart = _UnitOfWork._CartRepository.FindBy(c => c.CustomerId == customer.Id && c.Status == Status.open);
                    //first option user has cart but not close 
                    if (cart == null)
                    {
                        Cart CustomerCart = new Cart()
                        {
                            CustomerId = customer.Id,
                            RequestData = DateTime.Now,
                            Status = Status.open,
                        };
                        _UnitOfWork._CartRepository.Add(CustomerCart);
                        Item item = _UnitOfWork._ItemRepository.FindBy(i => i.Id == addCart.itemId);
                        CartItem cartItem = new CartItem() 
                        {
                            Item=item,
                            Cart= CustomerCart,
                            Quantity= addCart.quantity
                        };
                        _UnitOfWork._CartItemRepository.Add(cartItem);
                        _UnitOfWork.SaveChanges();
                    }
                    //second option user has not cart
                    else
                    {
                        CartItem CustomerCartItem = _UnitOfWork._CartItemRepository
                            .FindBy(c => c.ItemId == addCart.itemId && c.CartId==cart.Id);
                        //add different item in the cart
                        if (CustomerCartItem==null)
                        {
                            Item item=_UnitOfWork._ItemRepository.FindBy(i => i.Id == addCart.itemId);
                            CartItem cartItem = new CartItem()
                            {
                                Item = item,
                                Cart = cart,
                                Quantity = addCart.quantity
                            };
                            _UnitOfWork._CartItemRepository.Add(cartItem); 
                        }
                        //add same item in the cart
                        else
                        {
                            CustomerCartItem.Quantity += addCart.quantity;
                            _UnitOfWork._CartItemRepository.Update(CustomerCartItem);    
                        }
                        _UnitOfWork.SaveChanges();
                    }
                    result.Message="Item Add successfuly";
                    result.Succes = true;
                }
            }
            catch (DbUpdateException ex)
            {

            }


            return result;
        }
    }
}
