using System;

namespace WebApplication.ViewModels.Inputs.Life
{
    public class UpdateLifeViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
    }
}
