﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Shared.ViewModels
{
    public class AddCartViewModel
    {
        public int itemId { get; set; }
        public string customerEmail { get; set; }
        public int quantity { get; set; }
    }
}
