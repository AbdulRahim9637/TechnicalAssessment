using MediatR;
using Microsoft.AspNetCore.Mvc;
using Technical.Business.Application.Features.Bar.Command;
using Technical.Business.Application.Features.Bar.Query;
using Technical.Business.Domain;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechnicalAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarController : ControllerBase
    {
        private readonly ISender _mediator;

        public BarController(ISender mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<BarController>
        [HttpGet]
        public async Task<Response<GetAllBarsResponse>> Get()
        {
            var request = new GetAllBarsRequest();
            return await _mediator.Send(request);
        }

        // GET api/<BarController>/5
        [HttpGet("{id}")]
        public async Task<Response<GetBarResponse>> Get(Guid id)
        {
            var request=new GetBarRequest() { BarId= id };
            return await _mediator.Send(request);   
        }

        // POST api/<BarController>
        [HttpPost]
        public async Task<Response<AddBarResponse>> Post([FromBody] AddBarRequest request)
        {
            return await _mediator.Send(request);
        }

        // PUT api/<BarController>/5
        [HttpPut("{id}")]
        public async Task<Response<UpdateBarResponse>> Put(Guid id, [FromBody] UpdateBarRequest request)
        {
            return await _mediator.Send(request);
        }

    }
}
