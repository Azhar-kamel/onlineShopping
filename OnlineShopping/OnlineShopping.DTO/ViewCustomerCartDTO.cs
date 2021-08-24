using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.DTO
{
    public class ViewCustomerCartDTO
    {
        public int itemId { get; set; }
        public string itemName { get; set; }
        public string itemImage { get; set; }
        public int itemQuantity { get; set; }
        public decimal itemTax { get; set; }
        public decimal itemDiscount { get; set; }
        public decimal itemPrice { get; set; }
        public decimal itemTotalPrice { get; set; }
    }
}
