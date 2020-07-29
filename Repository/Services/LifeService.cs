using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository.Services
{
    public class LifeService : BaseRepository<Life>, ILifeService
    {
        public LifeService(ApplicationContext context) : base(context) { }

        public Task<List<Life>> GetByIntegrador(long integradorId)
        {
            return Set.Where(life => life.IntegradorId == integradorId).ToListAsync();
        }
    }
}