using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using X.PagedList;

namespace Repository.Services
{
    public class LifeService : BaseRepository<Life>, ILifeService
    {
        public LifeService(ApplicationContext context, IMapper mapper) : base(context, mapper) { }

        public Task<List<Life>> GetByIntegrator(long integratorId)
        {
            return Set.Where(life => life.IntegratorId == integratorId).ToListAsync();
        }

        public override Task<TR> FindByIdAs<TR>(long id)
        {
            return Set.Where(e => e.Id == id).Include(e => e.Steps).ProjectTo<TR>(mapper.ConfigurationProvider).FirstOrDefaultAsync();
        }

        public override Task<Life> FindById(long id)
        {
            return Set.Include(l => l.Steps).FirstOrDefaultAsync(l => l.Id == id);
        }
    }
}