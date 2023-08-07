using MediatR;
using Technical.Business.Data.Repository;
using Technical.Business.Domain;
using Technical.Business.Domain.Entity;

namespace Technical.Business.Application.Features.Beer.Command
{
    public class AddBeerHanlder:IRequestHandler<AddBeerRequest, Response<AddBeerResponse>>
    {
        private readonly IBeerRespository _beerRepository;
        public AddBeerHanlder(IBeerRespository beerRepository)
        {
            _beerRepository = beerRepository;
        }
        public async Task<Response<AddBeerResponse>> Handle(AddBeerRequest request, CancellationToken cancellationToken)
        {
            var response = await _beerRepository.AddBeerAsync(request, cancellationToken);
            return response.Handle(
                new AddBeerResponse
                {
                    BeerId = response.Model,
                }
            );
        }
    }

    public class AddBeerRequest : BeerModel, IRequest<Response<AddBeerResponse>> { }

    public class AddBeerResponse
    {
        public Guid BeerId { get; set; }

    }
}
