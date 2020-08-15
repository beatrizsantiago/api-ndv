using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository.Services
{
    public class LifeService : BaseRepository<Life>, ILifeService
    {
        public LifeService(ApplicationContext context, IMapper mapper) : base(context, mapper) { }

        public Task<List<Life>> GetByIntegrator(long integratorId)
        {
            return Set.Where(life => life.IntegratorId == integratorId).ToListAsync();
        }
    }
}