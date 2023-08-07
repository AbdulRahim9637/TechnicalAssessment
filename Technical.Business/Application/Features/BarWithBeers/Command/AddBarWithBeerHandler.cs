using MediatR;
using Technical.Business.Data.Repository;
using Technical.Business.Domain;

namespace Technical.Business.Application.Features.BarWithBeers.Command
{

    public class AddBarWithBeerHandler : IRequestHandler<AddBarWithBeerRequest, Response<AddBarWithBeerResponse>>
    {
        private readonly IBarBeersRepsoitory _repository;
        public AddBarWithBeerHandler(IBarBeersRepsoitory Repository)
        {
            _repository = Repository;
        }
        public async Task<Response<AddBarWithBeerResponse>> Handle(AddBarWithBeerRequest request, CancellationToken cancellationToken)
        {
            var response = await _repository.AddBarWithBeerAsync(request.BarId, request.BeerId, cancellationToken);
            return response.Handle(
                new AddBarWithBeerResponse
                {
                    BarWithBeerId = response.Model,
                }
            );
        }
    }

    public class AddBarWithBeerRequest : IRequest<Response<AddBarWithBeerResponse>>
    {
        public Guid BarId { get; set; }
        public Guid BeerId { get; set; }
    }

    public class AddBarWithBeerResponse
    {
        public Guid BarWithBeerId { get; set; }

    }
}
