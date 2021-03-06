using System;
using Domain.Enums;

namespace WebApplication.ViewModels.Inputs.Integration
{
    public class AddStepLifeViewModel {
        public long LifeId { get; set; }
        public StepsPropheticWay Step { get; set; }
        public DateTime Date { get; set; }
    }
}