using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Services.CartService;
using OnlineShopping.Shared.ViewModels;

namespace OnlineShopping.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _CartService;
        public CartController(ICartService CartService)
        {
            _CartService = CartService;
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddItemsToCart(AddCartViewModel addCart)
        {
            var result = _CartService.AddToCart(addCart);
            return Ok(result);
        }

        [HttpPost("{CustomerEmail}")]
        [Authorize]
        public IActionResult CheckoutCart(string CustomerEmail)
        {
            var result = _CartService.CheckoutCart(CustomerEmail);
            return Ok(result);
        }

        [HttpGet("{customerEmail}")]
        [Authorize]
        public IActionResult GetCustomerCart(string customerEmail)
        {
            var result = _CartService.GetCustomerCart(customerEmail);
            return Ok(result);
        }

        [HttpGet("{customerEmail}")]
        [Authorize]
        public IActionResult GetOrderList(string customerEmail)
        {
            var result = _CartService.GetOrderList(customerEmail);
            return Ok(result);
        }

        [HttpDelete]
        [Authorize]
        public IActionResult DeleteItemFromCart(DeleteFromCart deleteFromCart)
        {
            var result = _CartService.DeleteItemFromCart(deleteFromCart);
            return Ok(result);
        }
    }
}