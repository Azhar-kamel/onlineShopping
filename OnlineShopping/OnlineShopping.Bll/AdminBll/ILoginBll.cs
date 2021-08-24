using OnlineShopping.Entities;
using OnlineShopping.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Bll.AdminBll
{
    public interface ILoginBll
    {
        ResultViewModel Authenticate(loginViewModel loginUser,Admin user);
    }
}
