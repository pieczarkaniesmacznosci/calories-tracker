﻿using Microsoft.AspNetCore.Identity;

namespace Tracly.Data
{
    public class Role : IdentityRole<int>
    {
        public string Description { get; set; }
    }
}
