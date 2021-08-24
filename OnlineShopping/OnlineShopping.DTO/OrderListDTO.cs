using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.DTO
{
    public class OrderListDTO
    {
        public List<ViewCustomerCartDTO> cartDTOs { get; set; } = new List<ViewCustomerCartDTO>();
        public DateTime RequestData { get; set; }
        public DateTime OrderData { get; set; }
        public DateTime RecievedData { get; set; }
        public int CartId { get; set; }
        public string ItemsName { get; set; }
    }
}
