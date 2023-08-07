using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technical.Business.Data.Repository;
using Technical.Business.Domain.Entity;
using Technical.Business.Domain;

namespace Technical.Business.Application.Features.BrewaryWihBeers.Query
{
    public class GetAllBeweriesWithBeersHandler : IRequestHandler<GetAllBrewiesWithBeersRequest, Response<GetAllBreweriesWithBeersResponse>>
    {
        private readonly IBreweryBeersRepsoitory _repository;
        public GetAllBeweriesWithBeersHandler(IBreweryBeersRepsoitory Repository)
        {
            _repository = Repository;
        }
        public async Task<Response<GetAllBreweriesWithBeersResponse>> Handle(GetAllBrewiesWithBeersRequest request, CancellationToken cancellationToken)
        {
            var response = await _repository.GetAllBrewariesWithBeersAsync(cancellationToken);
            return new Response<GetAllBreweriesWithBeersResponse>(new GetAllBreweriesWithBeersResponse() { Breweries = response.Model });
        }
    }

    public class GetAllBrewiesWithBeersRequest : IRequest<Response<GetAllBreweriesWithBeersResponse>> { }

    public class GetAllBreweriesWithBeersResponse
    {
        public IEnumerable<BreweryWithBeersModel> Breweries { get; set; }

    }
}
