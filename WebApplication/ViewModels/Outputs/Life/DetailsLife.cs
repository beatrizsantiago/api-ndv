using System;
using WebApplication.ViewModels.Output.Account;
using WebApplication.ViewModels.Output.Feedback;

namespace WebApplication.ViewModels.Output.Life
{
    public class DetailsLifeViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public PreviewIntegratorViewModel Integrator { get; set; }
        public PreviewFeedbackViewModel Feedbacks { get; set; }
        public ProgressStepsViewModel HistoricPropheticWay { get; set; }
        
    }
}