﻿using Microsoft.AspNetCore.Identity;

namespace DemoS2.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
