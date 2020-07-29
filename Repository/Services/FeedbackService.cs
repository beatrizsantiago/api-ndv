using AutoMapper;
using Domain.Entities;
using Repository.Interfaces;

namespace Repository.Services
{
    public class FeedbackService : BaseRepository<Feedback>, IFeedbackService
    {
        public FeedbackService(ApplicationContext context, IMapper mapper) : base(context, mapper) { }
    }
}