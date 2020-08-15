using System;
using Domain.Enums;

namespace WebApplication.ViewModels.Output.Life
{
    public class AddStepLifeViewModel {
        public long IdLife { get; set; }
        public StepsPropheticWay Step { get; set; }
        public DateTime Date { get; set; }
    }
}