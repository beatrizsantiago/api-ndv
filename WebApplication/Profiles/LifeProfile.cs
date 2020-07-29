using AutoMapper;
using Domain.Entities;
using WebApplication.ViewModels.Output.Life;

namespace WebApplication.Profiles
{
    public class LifeProfile : Profile
    {
        public LifeProfile()
        {
            CreateMap<Life, PreviewLifeViewModel>();
        }
    }
}