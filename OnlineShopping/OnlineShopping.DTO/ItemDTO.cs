using OnlineShopping.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.DTO
{
    public class ItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string ImageId { get; set; }
        public int Quantity { get; set; }
        public UOM UnitOfMeasure { get; set; }
        public decimal Price { get; set; }
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }
        public int DiscountId { get; set; }
    }
}
