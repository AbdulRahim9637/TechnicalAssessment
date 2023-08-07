using MediatR;
using Technical.Business.Data.Repository;
using Technical.Business.Domain;
using Technical.Business.Domain.Entity;

namespace Technical.Business.Application.Features.Bar.Command
{
    public class UpdateBarHandler : IRequestHandler<UpdateBarRequest, Response<UpdateBarResponse>>
    {
        private readonly IBarRespository _repository;
        public UpdateBarHandler(IBarRespository barRepository)
        {
            _repository = barRepository;
        }
        public async Task<Response<UpdateBarResponse>> Handle(UpdateBarRequest request, CancellationToken cancellationToken)
        {
            var response = await _repository.UpdateBarAsync(request, cancellationToken);
            return response.Handle(
                new UpdateBarResponse
                {
                    BarId = response.Model,

                }
            );
        }
    }

    public class UpdateBarRequest : BarModel, IRequest<Response<UpdateBarResponse>> { }

    public class UpdateBarResponse
    {
        public Guid BarId { get; set; }

    }
}
