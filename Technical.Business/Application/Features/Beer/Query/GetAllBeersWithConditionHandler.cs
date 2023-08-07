using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technical.Business.Data.Repository;
using Technical.Business.Domain.Entity;
using Technical.Business.Domain;

namespace Technical.Business.Application.Features.Beer.Query
{
    
    public class GetAllBeersWithConditionHandler : IRequestHandler<GetBeersWithConditionRequest, Response<GetBeersWithConditionResponse>>
    {
        private readonly IBeerRespository _beerRepository;
        public GetAllBeersWithConditionHandler(IBeerRespository beerRepository)
        {
            _beerRepository = beerRepository;
        }
        public async Task<Response<GetBeersWithConditionResponse>> Handle(GetBeersWithConditionRequest request, CancellationToken cancellationToken)
        {
            var response = await _beerRepository.GetAllBeersAsync(request.GtAlcoholByVolume,request.LtAlcoholByVolume, cancellationToken);
            return new Response<GetBeersWithConditionResponse>(new GetBeersWithConditionResponse() { Beers = response.Model });

        }
    }
    public class GetBeersWithConditionRequest : IRequest<Response<GetBeersWithConditionResponse>>
    {
        public decimal? GtAlcoholByVolume { get; set; }
        public decimal? LtAlcoholByVolume { get; set; }
    }

    public class GetBeersWithConditionResponse
    {
        public IEnumerable<BeerModel> Beers { get; set; }

    }
}
