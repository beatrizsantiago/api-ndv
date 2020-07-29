using System.Collections.Generic;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User : IdentityUser<long>
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public bool IsEnabled { get; set; }
        public string PhotoProfile { get; set; }
        public string Mentor { get; set; }
        public NumberNets Net { get; set; }

        public virtual List<Feedback> Feedbacks { get; set; }
        public virtual List<Life> Lifes { get; set; }
    }
}