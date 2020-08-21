using System;
using System.Collections.Generic;
using WebApplication.ViewModels.Output.Account;

namespace WebApplication.ViewModels.Output.Integration
{
    public class DetailsLifeViewModel
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public PreviewIntegratorViewModel Integrator { get; set; }
        public List<PreviewFeedbackViewModel> Feedbacks { get; set; }
        public List<ProgressStepsViewModel> HistoricPropheticWay { get; set; }
        
    }
}