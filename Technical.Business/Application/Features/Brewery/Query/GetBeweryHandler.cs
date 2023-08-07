using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technical.Business.Domain.Entity;
using Technical.Business.Domain;
using Technical.Business.Data.Repository;

namespace Technical.Business.Application.Features.Brewery.Query
{
    public class GetBeweryHandler : IRequestHandler<GetBreweryRequest, Response<GetBreweryResponse>>
    {
        private readonly IBreweryRepository _breweryRepository;
        public GetBeweryHandler(IBreweryRepository breweryRepository)
        {
            _breweryRepository = breweryRepository;
        }
        public async Task<Response<GetBreweryResponse>> Handle(GetBreweryRequest request, CancellationToken cancellationToken)
        {
            var response= await _breweryRepository.GetBreweryAsync(request.BeweryId, cancellationToken);
            return new Response<GetBreweryResponse>(new GetBreweryResponse() { Brewery = response.Model });
            
        }
    }
    public class GetBreweryRequest : IRequest<Response<GetBreweryResponse>>
    {
        public Guid BeweryId { get; set; }
    }

    public class GetBreweryResponse
    {
        public BreweryModel Brewery { get; set; }

    }
}
