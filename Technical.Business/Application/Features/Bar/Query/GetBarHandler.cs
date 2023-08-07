using MediatR;
using Technical.Business.Data.Repository;
using Technical.Business.Domain;
using Technical.Business.Domain.Entity;

namespace Technical.Business.Application.Features.Bar.Query
{
    public class GetBarHandler : IRequestHandler<GetBarRequest, Response<GetBarResponse>>
    {
        private readonly IBarRespository _repository;
        public GetBarHandler(IBarRespository barRepository)
        {
            _repository = barRepository;
        }
        public async Task<Response<GetBarResponse>> Handle(GetBarRequest request, CancellationToken cancellationToken)
        {
            var response = await _repository.GetBarAsync(request.BarId, cancellationToken);
            return new Response<GetBarResponse>(new GetBarResponse() { Bar = response.Model });

        }
    }
    public class GetBarRequest : IRequest<Response<GetBarResponse>>
    {
        public Guid BarId { get; set; }
    }

    public class GetBarResponse
    {
        public BarModel Bar { get; set; }

    }
}
