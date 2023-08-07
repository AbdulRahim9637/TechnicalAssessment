using MediatR;
using Microsoft.AspNetCore.Mvc;
using Technical.Business.Application.Features.Beer.Command;
using Technical.Business.Application.Features.Beer.Query;
using Technical.Business.Domain;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechnicalAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private readonly ISender _mediator;

        public BeerController(ISender mediator)
        {
            _mediator = mediator;
        }
       

        // GET api/<BeerController>/5
        [HttpGet("{id}")]
        public async Task<Response<GetBeerResponse>> Get(Guid id)
        {
            var request = new GetBeerRequest() { BeerId = id };
            return await _mediator.Send(request);
        }
        [HttpGet]
        public async Task<Response<GetBeersWithConditionResponse>> Get( decimal? gtAlcoholByVolume,decimal? ltAlcoholByVolume)
        {
            var request = new GetBeersWithConditionRequest() {  GtAlcoholByVolume=gtAlcoholByVolume,LtAlcoholByVolume=ltAlcoholByVolume };
            return await _mediator.Send(request);
        }


        // POST api/<BeerController>
        [HttpPost]
        public async Task<Response<AddBeerResponse>> Post([FromBody] AddBeerRequest request)
        {
            return await _mediator.Send(request);
        }

        // PUT api/<BeerController>/5
        [HttpPut("{id}")]
        public async Task<Response<UpdateBeerResponse>> Put(Guid id, [FromBody] UpdateBeerRequest request)
        {
            return await _mediator.Send(request);
        }

    }
}
