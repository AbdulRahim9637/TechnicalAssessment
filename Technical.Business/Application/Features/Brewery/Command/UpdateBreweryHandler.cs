using MediatR;
using Technical.Business.Data.Repository;
using Technical.Business.Domain;
using Technical.Business.Domain.Entity;

namespace Technical.Business.Application.Features.Brewery.Command
{
    public class UpdateBreweryHandler : IRequestHandler<UpdateBreweryRequest, Response<UpdateBreweryResponse>>
    {
        private readonly IBreweryRepository _breweryRepository;
        public UpdateBreweryHandler(IBreweryRepository breweryRepository)
        {
            _breweryRepository = breweryRepository;
        }
        public async Task<Response<UpdateBreweryResponse>> Handle(UpdateBreweryRequest request, CancellationToken cancellationToken)
        {
            var response = await _breweryRepository.UpdateBreweryAsync(request, cancellationToken);
            return response.Handle(
                new UpdateBreweryResponse
                {
                    BreweryId = response.Model,
                    
                }
            );
        }
    }

    public class UpdateBreweryRequest : BreweryModel, IRequest<Response<UpdateBreweryResponse>> { }

    public class UpdateBreweryResponse
    {
        public Guid BreweryId { get; set; }

    }
}
