using AutoMapper;
using BorrowedAPI.Models;
using BorrowedAPI.Repository;
using LibraryTransit.Contract.Dtos.Visitors;
using MassTransit;

namespace BorrowedAPI.DataTransit.VisitorCreateConsumer
{
    public class VisitorConsumer : IConsumer<IVisitorPublishDto>
    {
        private readonly IVisitorRepository _visitorRepo;
        private readonly IMapper _mapper;

        public VisitorConsumer(IVisitorRepository visitorRepo, IMapper mapper)
        {
            _visitorRepo = visitorRepo;
            _mapper = mapper;
        }
        public Task Consume(ConsumeContext<IVisitorPublishDto> context)
        {
            var visitor = _mapper.Map<Visitor>(context.Message);

            if (visitor is null)
                throw new ArgumentNullException("Visitor from received message must not be null");

            if (!_visitorRepo.IsVisitorExists(visitor.Id))
            {
                _visitorRepo.CreateVisitor(visitor);
                _visitorRepo.SaveChanges();
            }

            return Task.CompletedTask;
        }
    }
}
