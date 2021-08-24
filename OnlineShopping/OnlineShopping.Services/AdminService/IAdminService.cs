using OnlineShopping.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Services.AdminService
{
    public interface IAdminService
    {
        ResultViewModel Register(RegisterViewModel registerViewModel);
        ResultViewModel Login(loginViewModel loginUser);
    }
}
