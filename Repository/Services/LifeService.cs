using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Repository.Helpers;
using Repository.Interfaces;
using Repository.Models;
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

        public async Task<List<ReportStepsModel>> ReportStepsByPeriod(DateTime initDate, DateTime endDate)
        {
            var steps = Enum.GetValues(typeof(StepsPropheticWay)).Cast<StepsPropheticWay>().ToList();
            var list = new List<ReportStepsModel>();

            foreach (var step in steps)
            {
                var amount = await context.ProgressStepsLifes.Where(s => (s.DoneDate >= initDate && s.DoneDate <= endDate) && s.Step == step).CountAsync();
                list.Add(new ReportStepsModel{
                    Step = step,
                    StepName = EnumHelper<StepsPropheticWay>.GetDisplayValue(step),
                    AmoutStep = amount
                });
            }

            // var steps = await context.ProgressStepsLifes
            //     .Where(step => step.DoneDate >= initDate && step.DoneDate <= endDate)
            //     .GroupBy(step => step.Step)
            //     .Select(group => new ReportStepsModel{
            //         Step = group.Key,
            //         StepName = EnumHelper<StepsPropheticWay>.GetDisplayValue(group.Key),
            //         AmoutStep = group.Count()
            //     })
            //     .ToListAsync();

            return list;
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