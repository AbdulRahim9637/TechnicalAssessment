using MediatR;
using Microsoft.AspNetCore.Mvc;
using Technical.Business.Application.Features.Brewery.Command;
using Technical.Business.Application.Features.Brewery.Query;
using Technical.Business.Domain;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechnicalAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreweryController : ControllerBase
    {
        private readonly ISender _mediator;

        public BreweryController(ISender mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<BreweryController>
        [HttpGet]
        public async Task<Response<GetAllBreweriesResponse>> Get()
        {
            GetAllBreweryRequest request=new GetAllBreweryRequest();

            return await _mediator.Send(request);
        }

        // GET api/<BreweryController>/5
        [HttpGet("{id}")]
        public async Task<Response<GetBreweryResponse>> Get(Guid id)
        {
            GetBreweryRequest request= new GetBreweryRequest() { BeweryId= id };
            return await _mediator.Send(request);
        }

        // POST api/<BreweryController>
        [HttpPost]
        public async Task<Response<AddBreweryResponse>> Post([FromBody] AddBreweryRequest request)
        {
            return await _mediator.Send(request);
        }

        // PUT api/<BreweryController>/5
        [HttpPut("{id}")]
        public async Task<Response<UpdateBreweryResponse>> Put(Guid id, [FromBody] UpdateBreweryRequest request)
        {
            return await _mediator.Send(request);
        }

    }
}
