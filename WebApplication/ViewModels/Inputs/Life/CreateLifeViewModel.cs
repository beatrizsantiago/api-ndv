using System;

namespace WebApplication.ViewModels.Inputs.Life
{
    public class CreateLifeViewModel
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public bool IsBaptismOhterChurch { get; set; }
        public string MinisterBaptism { get; set; }
        
    }
}
