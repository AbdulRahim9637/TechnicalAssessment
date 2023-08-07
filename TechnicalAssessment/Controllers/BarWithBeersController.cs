using MediatR;
using Microsoft.AspNetCore.Mvc;
using Technical.Business.Application.Features.BarWithBeers.Command;
using Technical.Business.Application.Features.BarWithBeers.Query;
using Technical.Business.Domain;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechnicalAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarWithBeersController : ControllerBase
    {
        private readonly ISender _mediator;

        public BarWithBeersController(ISender mediator)
        {
            _mediator = mediator;
        }


        [HttpGet, Route("~/bar/beers")]
        public async Task<Response<GetAllBarsWithBeersResponse>> GetAll()
        {
            var request = new GetAllBarsWithBeersRequest();
            return await _mediator.Send(request);
        }

        //// GET api/<BarWithBeersController>/5
        [HttpGet, Route("~/bar/{barId}/beers")]
        public async Task<Response<GetBarWithBeersResponse>> Get(Guid barId)
        {
            var request = new GetBarWithBeersRequest() { BarId = barId };
            return await _mediator.Send(request);
        }

        // POST api/<BarWithBeersController>
        [HttpPost, Route("~/bar/beer")]
        public async Task<Response<AddBarWithBeerResponse>> Post([FromBody] AddBarWithBeerRequest request)
        {
            return await _mediator.Send(request);
        }
    }
}
