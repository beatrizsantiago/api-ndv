using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User : IdentityUser<long>
    {
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public string Mentor { get; set; }
    }
}