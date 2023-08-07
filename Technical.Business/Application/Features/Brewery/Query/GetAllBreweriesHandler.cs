using MediatR;
using Technical.Business.Data.Repository;
using Technical.Business.Domain;
using Technical.Business.Domain.Entity;

namespace Technical.Business.Application.Features.Brewery.Query
{
    public class GetAllBreweriesHandler : IRequestHandler<GetAllBreweryRequest, Response<GetAllBreweriesResponse>>
    {
        private readonly IBreweryRepository _breweryRepository;
        public GetAllBreweriesHandler(IBreweryRepository breweryRepository)
        {
            _breweryRepository = breweryRepository;
        }
        public async Task<Response<GetAllBreweriesResponse>> Handle(GetAllBreweryRequest request, CancellationToken cancellationToken)
        {
           var response= await _breweryRepository.GetAllBreweriesAsync(cancellationToken).ConfigureAwait(false);
            return new Response<GetAllBreweriesResponse>(new GetAllBreweriesResponse() { Breweries = response.Model });
        }
    }

    public class GetAllBreweryRequest :  IRequest<Response<GetAllBreweriesResponse>> { }

    public class GetAllBreweriesResponse
    {
        public IEnumerable<BreweryModel> Breweries { get; set; }

    }
}
