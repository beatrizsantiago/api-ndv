using System;

namespace WebApplication.ViewModels.Inputs.Integration
{
    public class UpdateLifeViewModel
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public long IntegratorId { get; set; }
    }
}
