using OnlineShopping.Bll.CartBll;
using OnlineShopping.Entities;
using OnlineShopping.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Services.CartService
{
    public class CartService: ICartService
    {
        private readonly IAddBll _AddBll;
        private readonly IGetBll _GetBll;
        private readonly ICheckoutBll _CheckoutBll;
        private readonly IDeleteBll _DeleteBll;
        private readonly IValidatioBll _ValidatioBll;
        private readonly IGetOrderListBll _GetOrderListBll;
        public CartService(IAddBll AddBll,
                           IGetBll GetBll,
                           IDeleteBll DeleteBll,
                           ICheckoutBll CheckoutBll,
                           IValidatioBll ValidatioBll,
                           IGetOrderListBll GetOrderListBll)
        {
            _AddBll = AddBll;
            _GetBll = GetBll;
            _DeleteBll = DeleteBll;
            _CheckoutBll = CheckoutBll;
            _ValidatioBll = ValidatioBll;
            _GetOrderListBll = GetOrderListBll;
        }

        public ResultViewModel AddToCart(AddCartViewModel addCart)
        {
            return _AddBll.AddToCart(addCart);
        }

        public ResultViewModel CheckoutCart(string CustomerEmail)
        {
            return _CheckoutBll.CheckoutCart(CustomerEmail);
        }

        public ResultViewModel DeleteItemFromCart(DeleteFromCart deleteItem)
        {
            return _DeleteBll.DeleteFromCart(deleteItem);
        }

        public ResultViewModel GetCustomerCart(string CustomerEmail)
        {
            return _GetBll.GetCart(CustomerEmail);
        }

        public ResultViewModel GetOrderList(string CustomerEmail)
        {
            ResultViewModel resultValidateCustomer = _ValidatioBll.ValidateUser(CustomerEmail);
            if(resultValidateCustomer.Succes)
            {
                return _GetOrderListBll.GetOrderList((Customer)resultValidateCustomer.ReturnObject);
            }
            return resultValidateCustomer;
        }
    }
}
