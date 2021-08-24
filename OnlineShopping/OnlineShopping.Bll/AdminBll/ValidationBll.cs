using OnlineShopping.Data.UnitOfWork;
using OnlineShopping.Entities;
using OnlineShopping.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Bll.AdminBll
{
    public class ValidationBll : IValidationBll
    {
        private readonly IUnitOfWork _UnitOfWork;
        public ValidationBll(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }
        public ResultViewModel CheckIsUserAlreadyExist(string Email)
        {
            ResultViewModel result = new ResultViewModel();
            Admin user = _UnitOfWork._AdminRepository.FindBy(a => a.Email == Email);
            if (user == null)
            {
                result.Succes = true;
                result.ReturnObject = user;
                return result;
            }
            result.Succes = false;
            result.ReturnObject = user;
            return result;
        }
    }
}
