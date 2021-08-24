using OnlineShopping.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Entities
{
    public class UnitOfMeasure
    {
        public int Id { get; set; }
        public UOM UOM { get; set; }
        public string Description { get; set; }
        public List<Item> Items { get; set; }
    }
}
