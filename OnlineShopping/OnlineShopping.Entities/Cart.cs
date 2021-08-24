using OnlineShopping.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public DateTime RequestData { get; set; }
        public DateTime OrderData { get; set; }
        public DateTime RecievedData { get; set; }
        public Status Status { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public List<CartItem> cartItems { get; set; }
    }
}
