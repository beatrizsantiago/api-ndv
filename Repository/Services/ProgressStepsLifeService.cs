using Domain.Entities;
using Repository.Interfaces;

namespace Repository.Services
{
    public class ProgressStepsLifeService : BaseRepository<ProgressStepsLife>, IProgressStepsLifeService
    {
        public ProgressStepsLifeService(ApplicationContext context) : base(context) { }
    }
}