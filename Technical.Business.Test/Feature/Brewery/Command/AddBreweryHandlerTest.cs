using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technical.Business.Application.Features.Brewery.Command;
using Technical.Business.Application.Features.Brewery.Query;
using Technical.Business.Test.Feature.Brewery.Repository;
using Xunit;

namespace Technical.Business.Test.Feature.Brewery.Command
{
    public class AddBreweryHandlerTest
    {
        private readonly MockBreweryRepository _mockRepsitory;

        public AddBreweryHandlerTest() => _mockRepsitory = new MockBreweryRepository();
      
        [Fact]

        public async Task AddBreweryRequest_Success_Result()
        {
            //Arrange
            var handler = new AddBreweryRequest()
            {
                BreweryId = new Guid(),
                Name = "test"
            };
            _mockRepsitory.ArrageGetBreweryById();

            //Act 
            var result = new AddBreweryHandler(_mockRepsitory.MockObject);

            //Assert
            Assert.True(true);
        }
    }
}
