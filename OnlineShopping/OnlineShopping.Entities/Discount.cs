using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Entities
{
    public class Discount
    {
        public int Id { get; set; }
        public decimal value { get; set; }
        public List<Item> Items { get; set; }
    }
}
