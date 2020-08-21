using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Repository.Models;

namespace Repository.Interfaces
{
    public interface ILifeService : IBaseRepository<Life>
    {
        Task<List<Life>> GetByIntegrator(long integratorId);
        Task<List<ReportStepsModel>> ReportStepsByPeriod(DateTime initDate, DateTime endDate);
    }
}