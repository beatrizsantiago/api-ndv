using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using WebApplication.ViewModels.Output.Account;
using WebApplication.ViewModels.Output.Feedback;
using WebApplication.ViewModels.Output.Life;

namespace WebApplication.Profiles
{
    public class LifeProfile : Profile
    {
        public LifeProfile()
        {
            CreateMap<Life, PreviewLifeViewModel>();
            CreateMap<Life, DetailsLifeViewModel>()
                .ForMember(dest => dest.HistoricPropheticWay, options => options.MapFrom(src => GetList(src)));
            CreateMap<User, PreviewIntegratorViewModel>();
            CreateMap<Feedback, PreviewFeedbackViewModel>()
                .ForMember(dest => dest.Integrator, options => options.MapFrom(src => src.Integrator.Name));
            CreateMap<ProgressStepsLife, ProgressStepsViewModel>();
        }

        public static List<ProgressStepsViewModel> GetList(Life life)
        {
            var steps = Enum.GetValues(typeof(StepsPropheticWay)).Cast<StepsPropheticWay>().Select(e => new ProgressStepsViewModel(){Step = e}).ToList();
            var lifeSteps = life.Steps;

            foreach (var step in steps)
            {
                var currentMap = lifeSteps.FirstOrDefault(ls => ls.Step == step.Step);
                if(currentMap is null) continue;

                if(currentMap.DoneDate != DateTime.MinValue)
                    step.Date = currentMap.DoneDate;
            }

            return steps;
        }
    }
}