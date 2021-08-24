using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Services.AdminService;
using OnlineShopping.Shared.ViewModels;

namespace OnlineShopping.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _IAdminService;
        public AdminController(IAdminService IAdminService)
        {
            _IAdminService = IAdminService;
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel register)
        {
            var result = _IAdminService.Register(register);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult login(loginViewModel loginUser)
        {
            var result = _IAdminService.Login(loginUser);
            return Ok(result);
        }
    }
}