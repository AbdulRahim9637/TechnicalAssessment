using MediatR;
using Technical.Business.Data.Repository;
using Technical.Business.Domain;
using Technical.Business.Domain.Entity;

namespace Technical.Business.Application.Features.BarWithBeers.Query
{

    public class GetBarWIthBeersHandler : IRequestHandler<GetBarWithBeersRequest, Response<GetBarWithBeersResponse>>
    {
        private readonly IBarBeersRepsoitory _repository;
        public GetBarWIthBeersHandler(IBarBeersRepsoitory Repository)
        {
            _repository = Repository;
        }
        public async Task<Response<GetBarWithBeersResponse>> Handle(GetBarWithBeersRequest request, CancellationToken cancellationToken)
        {
            var response = await _repository.GetBarWithBeersAsync(request.BarId, cancellationToken);
            return new Response<GetBarWithBeersResponse>(new GetBarWithBeersResponse() { Bar = response.Model });

        }
    }
    public class GetBarWithBeersRequest : IRequest<Response<GetBarWithBeersResponse>>
    {
        public Guid BarId { get; set; }
    }

    public class GetBarWithBeersResponse
    {
        public BarWithBeersModel Bar { get; set; }

    }
}
