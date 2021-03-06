using System;
using Domain.Enums;

namespace WebApplication.ViewModels.Output.Integration
{
    public class ProgressStepsViewModel
    {
        public ProgressStepsViewModel()
        {
            
        }

        public ProgressStepsViewModel(DateTime date)
        {
            this.Date = date;
        }

        public DateTime? Date { get; set; }
        public StepsPropheticWay Step { get; set; }
    }
}