using Domain.Enums;

namespace WebApplication.ViewModels.Output.Integration
{
    public class PreviewLifeViewModel
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public LegendLife Legend { get; set; }
    }
}