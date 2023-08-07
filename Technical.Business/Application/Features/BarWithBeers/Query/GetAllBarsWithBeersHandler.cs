using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technical.Business.Data.Repository;
using Technical.Business.Domain.Entity;
using Technical.Business.Domain;

namespace Technical.Business.Application.Features.BarWithBeers.Query
{
    public class GetAllBarsWithBeersHandler : IRequestHandler<GetAllBarsWithBeersRequest, Response<GetAllBarsWithBeersResponse>>
    {
        private readonly IBarBeersRepsoitory _repository;
        public GetAllBarsWithBeersHandler(IBarBeersRepsoitory Repository)
        {
            _repository = Repository;
        }
        public async Task<Response<GetAllBarsWithBeersResponse>> Handle(GetAllBarsWithBeersRequest request, CancellationToken cancellationToken)
        {
            var response = await _repository.GetAllBarsWithBeersAsync(cancellationToken);
            return new Response<GetAllBarsWithBeersResponse>(new GetAllBarsWithBeersResponse() { Bars = response.Model });
        }
    }

    public class GetAllBarsWithBeersRequest : IRequest<Response<GetAllBarsWithBeersResponse>> { }

    public class GetAllBarsWithBeersResponse
    {
        public IEnumerable<BarWithBeersModel> Bars { get; set; }

    }

}
