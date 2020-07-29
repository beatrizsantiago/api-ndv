using AutoMapper;
using Domain.Entities;
using Repository.Interfaces;

namespace Repository.Services
{
    public class ProgressStepsLifeService : BaseRepository<ProgressStepsLife>, IProgressStepsLifeService
    {
        public ProgressStepsLifeService(ApplicationContext context, IMapper mapper) : base(context, mapper) { }
    }
}