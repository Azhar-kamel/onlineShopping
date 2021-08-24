using Microsoft.EntityFrameworkCore;
using OnlineShopping.Data.UnitOfWork;
using OnlineShopping.DTO;
using OnlineShopping.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Bll.ItemBll
{
    public class GetItemsBll: IGetItemsBll
    {
        private readonly IUnitOfWork _UnitOfWork;
        public GetItemsBll(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }

        public ResultViewModel ReturnItems()
        {
            ResultViewModel result = new ResultViewModel() 
            { 
                Message="There is no items",
                Succes=false
            };
            var items = _UnitOfWork._ItemRepository.GetAllAsQueryable()
                .Include(b => b.Tax)
                .Include(c => c.UnitOfMeasure)
                .Select(d => new ItemDTO()
                {
                    Id=d.Id,
                    CompanyName=d.CompanyName,
                    Name=d.Name,
                    UnitOfMeasure=d.UnitOfMeasure.UOM,
                    ImageId=d.ImageId,
                    Price=d.price,
                    Quantity=d.Quantity,
                    Tax=d.Tax.value,
                    DiscountId=d.DiscountId==null?0:(int)d.DiscountId
                }).ToList();
            foreach(var item in items)
            {
                if(item.DiscountId !=0)
                {
                    item.Discount = _UnitOfWork._DiscountRepository
                        .Find(a => a.Id == item.DiscountId)
                        .Select(a => a.value).FirstOrDefault();
                }
                    
            }
            if(items.Count()>0)
            {
                result.Message = "There is items";
                result.Succes = true;
                result.ReturnObject = items;
            }
            return result;
        }
    }
}
