using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Shared.ViewModels
{
    public class ResultViewModel
    {
        public bool Succes { get; set; }
        public string Message { get; set; }
        public object ReturnObject { get; set; }
    }
}
