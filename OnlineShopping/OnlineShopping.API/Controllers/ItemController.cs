using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Services.ItemService;

namespace OnlineShopping.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _ItemService;
        public ItemController(IItemService ItemService)
        {
            _ItemService = ItemService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetItems()
        {
            var result = _ItemService.GetItems();
            return Ok(result);
        }
    }
}