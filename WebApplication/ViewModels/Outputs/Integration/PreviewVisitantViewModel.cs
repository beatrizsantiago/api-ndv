using Domain.Enums;

namespace WebApplication.ViewModels.Output.Integration
{
    public class PreviewVisitantViewModel
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public bool FrequentOtherChurch { get; set; }
        public string Companion { get; set; }
    }
}