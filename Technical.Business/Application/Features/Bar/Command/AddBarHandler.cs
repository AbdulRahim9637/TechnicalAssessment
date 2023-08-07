using MediatR;
using Technical.Business.Data.Repository;
using Technical.Business.Domain;
using Technical.Business.Domain.Entity;

namespace Technical.Business.Application.Features.Bar.Command
{
    public class AddBarHandler : IRequestHandler<AddBarRequest, Response<AddBarResponse>>
    {
        private readonly IBarRespository _repository;
        public AddBarHandler(IBarRespository barRepository)
        {
            _repository = barRepository;
        }
        public async Task<Response<AddBarResponse>> Handle(AddBarRequest request, CancellationToken cancellationToken)
        {
            var response = await _repository.AddBarAsync(request, cancellationToken);
            return response.Handle(
                new AddBarResponse
                {
                    BarId = response.Model,
                }
            );
        }
    }

    public class AddBarRequest : BarModel, IRequest<Response<AddBarResponse>> { }

    public class AddBarResponse
    {
        public Guid BarId { get; set; }

    }
}
