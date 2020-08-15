using System;
using Domain.Enums;

namespace WebApplication.ViewModels.Inputs.Life
{
    public class CreateLifeViewModel
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public bool BaptismOtherChurch { get; set; }
        public string BaptismMinister { get; set; }
        public bool BaptismToday { get; set; }
        public StepsPropheticWay TypeConversion { get; set; } 
        
    }
}
