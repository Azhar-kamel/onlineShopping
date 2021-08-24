using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Entities
{
    public class Admin:IdentityUser
    {
        [Required]
        public string FullName { get; set; }
        public bool IsBlocked { get; set; } = false;

    }
}
