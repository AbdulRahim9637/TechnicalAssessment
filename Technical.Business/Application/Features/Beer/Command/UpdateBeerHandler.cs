using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technical.Business.Data.Repository;
using Technical.Business.Domain.Entity;
using Technical.Business.Domain;

namespace Technical.Business.Application.Features.Beer.Command
{
    
    public class UpdateBeerHandler : IRequestHandler<UpdateBeerRequest, Response<UpdateBeerResponse>>
    {

        private readonly IBeerRespository _beerRepository;
        public UpdateBeerHandler(IBeerRespository beerRepository)
        {
            _beerRepository = beerRepository;
        }
        public async Task<Response<UpdateBeerResponse>> Handle(UpdateBeerRequest request, CancellationToken cancellationToken)
        {
            var response = await _beerRepository.UpdateBeerAsync(request, cancellationToken);
            return response.Handle(
                new UpdateBeerResponse
                {
                    BeerId = response.Model,

                }
            );
        }
    }

    public class UpdateBeerRequest : BeerModel, IRequest<Response<UpdateBeerResponse>> { }

    public class UpdateBeerResponse
    {
        public Guid BeerId { get; set; }

    }
}
