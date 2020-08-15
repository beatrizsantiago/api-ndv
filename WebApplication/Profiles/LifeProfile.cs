using AutoMapper;
using Domain.Entities;
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
                .ForMember(dest => dest.HistoricPropheticWay, options => options.MapFrom(src => src.Steps));
            CreateMap<User, PreviewIntegratorViewModel>();
            CreateMap<Feedback, PreviewFeedbackViewModel>()
                .ForMember(dest => dest.Integrator, options => options.MapFrom(src => src.Integrator.Name));
            CreateMap<ProgressStepsLife, ProgressStepsViewModel>();
        }
    }
}