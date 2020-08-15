using System;
using Domain.Enums;

namespace WebApplication.ViewModels.Output.Life
{
    public class ProgressStepsViewModel
    {
        public DateTime Date { get; set; }
        public StepsPropheticWay Step { get; set; }
    }
}