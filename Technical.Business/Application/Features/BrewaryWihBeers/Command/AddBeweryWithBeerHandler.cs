using MediatR;
using Technical.Business.Data.Repository;
using Technical.Business.Domain;

namespace Technical.Business.Application.Features.BrewaryWihBeers.Command
{
    public class AddBeweryWithBeerHandler : IRequestHandler<AddBrewaryWithBeerRequest, Response<AddBrewaryWithBeerWithBeerResponse>>
    {
        private readonly IBreweryBeersRepsoitory _repository;
        public AddBeweryWithBeerHandler(IBreweryBeersRepsoitory Repository)
        {
            _repository = Repository;
        }
        public async Task<Response<AddBrewaryWithBeerWithBeerResponse>> Handle(AddBrewaryWithBeerRequest request, CancellationToken cancellationToken)
        {
            var response = await _repository.AddBreweryWithBeerAsync(request.BrewaryId,request.BeerId, cancellationToken);
            return response.Handle(
                new AddBrewaryWithBeerWithBeerResponse
                {
                    BrewaryWithBeerId = response.Model,
                }
            );
        }
    }

    public class AddBrewaryWithBeerRequest : IRequest<Response<AddBrewaryWithBeerWithBeerResponse>>
    {
        public Guid BrewaryId { get; set; }
        public Guid BeerId { get; set; }
    }

    public class AddBrewaryWithBeerWithBeerResponse
    {
        public Guid BrewaryWithBeerId { get; set; }

    }
}
