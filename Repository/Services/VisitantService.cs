using AutoMapper;
using Domain.Entities;
using Repository.Interfaces;

namespace Repository.Services
{
    public class VisitantService : BaseRepository<Visitant>, IVisitantService
    {
        public VisitantService(ApplicationContext context, IMapper mapper) : base(context, mapper) { }
    }
}