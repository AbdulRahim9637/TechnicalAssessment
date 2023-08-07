using MediatR;
using Technical.Business.Data.Repository;
using Technical.Business.Domain.Entity;
using Technical.Business.Domain;

namespace Technical.Business.Application.Features.BrewaryWihBeers.Query
{
    public class GetBeweryWithBeersHandler : IRequestHandler<GetBreweryWithBeersRequest, Response<GetBreweryWithBeersResponse>>
    {
        private readonly IBreweryBeersRepsoitory _repository;
        public GetBeweryWithBeersHandler(IBreweryBeersRepsoitory Repository)
        {
            _repository = Repository;
        }
        public async Task<Response<GetBreweryWithBeersResponse>> Handle(GetBreweryWithBeersRequest request, CancellationToken cancellationToken)
        {
            var response = await _repository.GetBreweryWithBeersAsync(request.BeweryId, cancellationToken);
            return new Response<GetBreweryWithBeersResponse>(new GetBreweryWithBeersResponse() { Brewery = response.Model });

        }
    }
    public class GetBreweryWithBeersRequest : IRequest<Response<GetBreweryWithBeersResponse>>
    {
        public Guid BeweryId { get; set; }
    }

    public class GetBreweryWithBeersResponse
    {
        public BreweryWithBeersModel Brewery { get; set; }

    }
}
