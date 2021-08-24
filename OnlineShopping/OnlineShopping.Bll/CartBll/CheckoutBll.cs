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
    public class CheckoutBll : ICheckoutBll
    {
        private readonly IUnitOfWork _UnitOfWork;
        public CheckoutBll(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }
        public ResultViewModel CheckoutCart(string CustomerEmail)
        {
            ResultViewModel result = new ResultViewModel()
            {
                Message = "There is no cart in your account",
                Succes = false
            };

            Customer customer = _UnitOfWork._CustomerRepository.FindBy(c => c.Email == CustomerEmail);
            if (customer != null)
            {
                Cart cart = _UnitOfWork._CartRepository.FindBy(c => c.CustomerId == customer.Id && c.Status == Status.open);
                if (cart != null)
                {
                    cart.Status = Status.close;
                    cart.OrderData = DateTime.Now;
                    cart.RecievedData = DateTime.Now.AddDays(3);
                    _UnitOfWork._CartRepository.Update(cart);
                    _UnitOfWork.SaveChanges();
                    result.Message = "your cart checkout successfuly";
                    result.Succes = true;
                }
            }
            return result;
        }
    }
}
