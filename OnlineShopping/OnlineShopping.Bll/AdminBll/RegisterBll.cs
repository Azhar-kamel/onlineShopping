using Microsoft.AspNetCore.Identity;
using OnlineShopping.Data.UnitOfWork;
using OnlineShopping.Entities;
using OnlineShopping.Shared;
using OnlineShopping.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Bll.AdminBll
{
    public class RegisterBll : IRegisterBll
    {
        private readonly UserManager<Admin> _UserManger;
        private readonly IUnitOfWork _UnitOfWork;
        public RegisterBll(UserManager<Admin> UserManger, IUnitOfWork UnitOfWork)
        {
            _UserManger = UserManger;
            _UnitOfWork = UnitOfWork;
        }
        public ResultViewModel RegisterUser(RegisterViewModel registerViewModel)
        {
            ResultViewModel result = new ResultViewModel()
            {
                Message = "Register Fail",
                Succes = false
            };
            Admin user = new Admin();
            user.Email = registerViewModel.Email;
            user.FullName = registerViewModel.FullName;
            user.UserName= registerViewModel.Email;

            if (registerViewModel.Role == Roles.Admin)
            {
                var AddAdminUser = _UserManger.CreateAsync(user, registerViewModel.Password)
                                           .GetAwaiter()
                                           .GetResult();
                if(AddAdminUser.Succeeded)
                {
                    _UserManger.AddToRoleAsync(user, "Admin").GetAwaiter().GetResult();
                    result.Message = "Add Admin Successfuly";
                    result.Succes = true;
                }
            }
            else
            {
                var AddCustomerUser = _UserManger.CreateAsync(user, registerViewModel.Password)
                                           .GetAwaiter()
                                           .GetResult();
                if (AddCustomerUser.Succeeded)
                {
                    _UserManger.AddToRoleAsync(user, "Customer").GetAwaiter().GetResult();
                    Customer customer = new Customer() 
                    { 
                        age= registerViewModel.Age,
                        Email= registerViewModel.Email,
                        Name= registerViewModel.FullName
                    };

                    _UnitOfWork._CustomerRepository.Add(customer);
                    _UnitOfWork.SaveChanges();
                    result.Message = "Add Customer Successfuly";
                    result.Succes = true;
                }
            }
            return result;
        }
    }
}
