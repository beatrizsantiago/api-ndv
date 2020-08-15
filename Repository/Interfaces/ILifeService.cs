using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Repository.Interfaces
{
    public interface ILifeService : IBaseRepository<Life>
    {
        Task<List<Life>> GetByIntegrator(long integratorId);
    }
}