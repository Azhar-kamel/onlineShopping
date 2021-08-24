using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Entities
{
    public class Item
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string MadeBy { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public string ImageId { get; set; }
        public int Quantity { get; set; }
        public int UnitOfMeasureId { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }

        public decimal price { get; set; }
        public int TaxId { get; set; }
        public Tax Tax { get; set; }

        public int? DiscountId { get; set; }
        public Discount Discount { get; set; }

        public List<CartItem> cartItems { get; set; }
    }
}
