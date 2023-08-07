using MediatR;
using Microsoft.AspNetCore.Mvc;
using Technical.Business.Application.Features.BrewaryWihBeers.Command;
using Technical.Business.Application.Features.BrewaryWihBeers.Query;
using Technical.Business.Domain;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechnicalAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreweryWithBeersController : ControllerBase
    {
        private readonly ISender _mediator;

        public BreweryWithBeersController(ISender mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<BrewaryWihBeersController>
        [HttpGet, Route("~/brewery/{breweryId}/beers")]
        public async Task<Response<GetBreweryWithBeersResponse>> Get(Guid breweryId)
        {
            var request=new GetBreweryWithBeersRequest() {  BeweryId= breweryId };
            return await _mediator.Send(request);
        }
        // GETAll: api/<BrewaryWihBeersController>
        [HttpGet, Route("~/brewery/beers")]
        public async Task<Response<GetAllBreweriesWithBeersResponse>> GetAll()
        {
            var request = new GetAllBrewiesWithBeersRequest();
            return await _mediator.Send(request);
        }

        // POST api/<BrewaryWihBeersController>
        [HttpPost,Route("~/brewery/beer")]
        public async Task<Response<AddBrewaryWithBeerWithBeerResponse>> Post([FromBody] AddBrewaryWithBeerRequest request)
        {
            return await _mediator.Send(request);
        }

       
    }
}
