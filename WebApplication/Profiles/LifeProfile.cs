using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using WebApplication.ViewModels.Output.Account;
using WebApplication.ViewModels.Output.Integration;

namespace WebApplication.Profiles
{
    public class LifeProfile : Profile
    {
        public LifeProfile()
        {
            CreateMap<Life, PreviewLifeViewModel>()
                .ForMember(dest => dest.Legend, options => options.MapFrom(src => GetLegend(src)));
            CreateMap<Life, DetailsLifeViewModel>()
                .ForMember(dest => dest.HistoricPropheticWay, options => options.MapFrom(src => GetList(src)));
            CreateMap<User, PreviewIntegratorViewModel>();
            CreateMap<Feedback, PreviewFeedbackViewModel>()
                .ForMember(dest => dest.Integrator, options => options.MapFrom(src => src.Integrator.Name));
            CreateMap<ProgressStepsLife, ProgressStepsViewModel>();
            CreateMap<Visitant, PreviewVisitantViewModel>();
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

            var stepConverted = steps.First(s => s.Step == StepsPropheticWay.Converted);
            var stepReconsiliated = steps.First(s => s.Step == StepsPropheticWay.Reconsiliated);

            if (stepReconsiliated.Date is null)
            {
                steps.Remove(stepReconsiliated);
            }
            else
            {
                steps.Remove(stepConverted);
            }

            return steps;
        }

        public static LegendLife GetLegend(Life life)
        {
            var steps = life.Steps;
            var actualStep = steps.OrderBy(step => step.Step).First();

            if (life.IsLost)
            {
                return LegendLife.LostLife;
            }
            else if (life.Age > 0 && life.Age <= 10)
            {
                return LegendLife.IsChild;
            }
            else if (actualStep.Step == StepsPropheticWay.ClassChristianLife || actualStep.Step == StepsPropheticWay.ClassLeaderCap)
            {
                return LegendLife.InClasses;
            }
            else
            {
                return LegendLife.None;
            }
        }
    }
}