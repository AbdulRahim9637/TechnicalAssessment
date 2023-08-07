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
  
    public class GetBeerByBeerIdHandler : IRequestHandler<GetBeerRequest, Response<GetBeerResponse>>
    {
        private readonly IBeerRespository _beerRepository;
        public GetBeerByBeerIdHandler(IBeerRespository beerRepository)
        {
            _beerRepository = beerRepository;
        }
        public async Task<Response<GetBeerResponse>> Handle(GetBeerRequest request, CancellationToken cancellationToken)
        {
            var response = await _beerRepository.GetBeerAsync(request.BeerId, cancellationToken);
            return new Response<GetBeerResponse>(new GetBeerResponse() { Beer = response.Model });

        }
    }
    public class GetBeerRequest : IRequest<Response<GetBeerResponse>>
    {
        public Guid BeerId { get; set; }
    }

    public class GetBeerResponse
    {
        public BeerModel Beer { get; set; }

    }
}
