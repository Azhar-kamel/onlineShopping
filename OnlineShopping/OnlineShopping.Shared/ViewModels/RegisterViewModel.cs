using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Shared.ViewModels
{
    public class RegisterViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Roles Role { get; set; }
        public int Age { get; set; }
    }
}
