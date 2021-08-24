using OnlineShopping.Bll.AdminBll;
using OnlineShopping.Entities;
using OnlineShopping.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Services.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly IRegisterBll _IRegisterBll;
        private readonly IValidationBll _IValidationBll;
        private readonly ILoginBll _ILoginBll;
        public AdminService(IRegisterBll IRegisterBll, 
                            IValidationBll IValidationBll,
                            ILoginBll ILoginBll)
        {
            _IRegisterBll = IRegisterBll;
            _IValidationBll = IValidationBll;
            _ILoginBll = ILoginBll;
        }

        public ResultViewModel Login(loginViewModel loginUser)
        {
            ResultViewModel Validation = _IValidationBll.CheckIsUserAlreadyExist(loginUser.Email);
            if (!Validation.Succes)
                return _ILoginBll.Authenticate(loginUser,(Admin) Validation.ReturnObject);
            ResultViewModel result = new ResultViewModel()
            {
                Message = "This no user by this account",
                Succes = false
            };
            return result;
        }

        public ResultViewModel Register(RegisterViewModel registerViewModel)
        {
            ResultViewModel Validation = _IValidationBll.CheckIsUserAlreadyExist(registerViewModel.Email);
            if (Validation.Succes)
                return _IRegisterBll.RegisterUser(registerViewModel);

            ResultViewModel result = new ResultViewModel()
            {
                Message="This user is already exist",
                Succes=false
            };
            return result;
        }
    }
}
