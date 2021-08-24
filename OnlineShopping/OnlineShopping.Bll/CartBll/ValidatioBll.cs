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
    public class ValidatioBll : IValidatioBll
    {
        private readonly IUnitOfWork _UnitOfWork;
        public ValidatioBll(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }

        public ResultViewModel ValidateCartIsOpen(Customer customer)
        {
            ResultViewModel result = new ResultViewModel()
            {
                Message = "Cart is not Open",
                Succes = false
            };
            Cart cart = _UnitOfWork._CartRepository.FindBy(c => c.CustomerId == customer.Id && c.Status == Status.open);
            if (cart != null)
            {
                result.Message = "Cart is Open";
                result.Succes = true;
                result.ReturnObject = cart;
            }
            return result;
        }

        public ResultViewModel ValidateUser(string CustomerEmail)
        {
            ResultViewModel result = new ResultViewModel()
            {
                Message = "There is no Customer by this email",
                Succes = false
            };
            Customer customer = _UnitOfWork._CustomerRepository.FindBy(c => c.Email == CustomerEmail);
            if (customer != null)
            {
                result.Message = "There is Customer by this email";
                result.Succes = true;
                result.ReturnObject = customer;
            }
            return result;
        }
    }
}
