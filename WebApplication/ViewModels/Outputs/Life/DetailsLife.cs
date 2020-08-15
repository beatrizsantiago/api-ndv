using System;
using System.Collections.Generic;
using WebApplication.ViewModels.Output.Account;
using WebApplication.ViewModels.Output.Feedback;

namespace WebApplication.ViewModels.Output.Life
{
    public class DetailsLifeViewModel
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public PreviewIntegratorViewModel Integrator { get; set; }
        public List<PreviewFeedbackViewModel> Feedbacks { get; set; }
        public List<ProgressStepsViewModel> HistoricPropheticWay { get; set; }
        
    }
}