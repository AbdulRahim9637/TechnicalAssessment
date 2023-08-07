using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technical.Business.Data.Repository;
using Technical.Business.Domain.Entity;
using Technical.Business.Domain;

namespace Technical.Business.Application.Features.Bar.Query
{
    public class GetAllBarsHandler : IRequestHandler<GetAllBarsRequest, Response<GetAllBarsResponse>>
    {
        private readonly IBarRespository _repository;
        public GetAllBarsHandler(IBarRespository barRepository)
        {
            _repository = barRepository;
        }
        public async Task<Response<GetAllBarsResponse>> Handle(GetAllBarsRequest request, CancellationToken cancellationToken)
        {
            var response = await _repository.GetAllBarsAsync(cancellationToken).ConfigureAwait(false);
            return new Response<GetAllBarsResponse>(new GetAllBarsResponse() { Bars = response.Model });
        }
    }

    public class GetAllBarsRequest : IRequest<Response<GetAllBarsResponse>> { }

    public class GetAllBarsResponse
    {
        public IEnumerable<BarModel> Bars { get; set; }

    }
}
