using MediatR;
using Technical.Business.Data.Repository;
using Technical.Business.Domain;
using Technical.Business.Domain.Entity;

namespace Technical.Business.Application.Features.Brewery.Command
{
    public class AddBreweryHandler : IRequestHandler<AddBreweryRequest, Response<AddBreweryResponse>>
    {
        private readonly IBreweryRepository _breweryRepository;
        public AddBreweryHandler(IBreweryRepository breweryRepository)
        {
            _breweryRepository = breweryRepository;
        }
        public async Task<Response<AddBreweryResponse>> Handle(AddBreweryRequest request, CancellationToken cancellationToken)
        {
            var response = await _breweryRepository.AddBreweryAsync(request, cancellationToken);
            return response.Handle(
                new AddBreweryResponse
                {
                    BreweryId = response.Model,
                }
            );
        }
    }

    public class AddBreweryRequest : BreweryModel, IRequest<Response<AddBreweryResponse>> { }

    public class AddBreweryResponse
    {
        public Guid BreweryId { get; set; }

    }
}
