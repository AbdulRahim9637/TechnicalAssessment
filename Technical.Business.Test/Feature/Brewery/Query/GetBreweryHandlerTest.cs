using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technical.Business.Application.Features.BrewaryWihBeers.Query;
using Technical.Business.Application.Features.Brewery.Query;
using Technical.Business.Test.Feature.Brewery.Repository;
using Xunit;

namespace Technical.Business.Test.Feature.Brewery.Query
{
    public class GetBreweryHandlerTest
    {
        private readonly MockBreweryRepository _mockRepsitory;

        public GetBreweryHandlerTest() => _mockRepsitory = new MockBreweryRepository(); 
        [Theory]
        [InlineData("465babcd-0e17-4b0c-8e60-55c966963465")]
        public async Task GetBreweryRequest_Success_Result(Guid BreweryId)
        {
            //Arrange
            var handler = new GetBeweryHandler(_mockRepsitory.MockObject);
            _mockRepsitory.ArrageGetBreweryById();

            //Act 
            var result = await handler.Handle(new GetBreweryRequest { BeweryId= BreweryId }, CancellationToken.None);

            //Assert
            Assert.IsType<GetBreweryResponse>(result.Model);
        }
    }
}
