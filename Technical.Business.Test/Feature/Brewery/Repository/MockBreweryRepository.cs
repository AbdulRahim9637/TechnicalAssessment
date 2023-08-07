using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technical.Business.Data.Repository;
using Technical.Business.Domain;
using Technical.Business.Domain.Entity;

namespace Technical.Business.Test.Feature.Brewery.Repository
{
    internal class MockBreweryRepository
    {
        public IBreweryRepository MockObject  { get { return mockRepo.Object; } }

        public Mock<IBreweryRepository> mockRepo;
        public List<BreweryModel> breweryModels = new List<BreweryModel>()
        {
            new BreweryModel()
            {
                 BreweryId=new Guid("465babcd-0e17-4b0c-8e60-55c966963465"),
                 Name="Brewery"
            },
            new BreweryModel()
            {
                BreweryId=Guid.NewGuid(),
                Name="Brewery1"
            }
        };
        public MockBreweryRepository()
        {
            mockRepo=new Mock<IBreweryRepository>();
        }

        public Response<BreweryModel> GetBreweryModel()
        {
            return new Response<BreweryModel>(breweryModels.FirstOrDefault());
        }

        public void ArrageAddBrewery() => mockRepo.Setup(x => x.AddBreweryAsync(It.IsAny<Domain.Entity.BreweryModel>(), CancellationToken.None))
                                                        .ReturnsAsync(new Response<Guid>(Guid.NewGuid()));

        public void ArrageUpdateBrewery() => mockRepo.Setup(x => x.UpdateBreweryAsync(It.IsAny<Domain.Entity.BreweryModel>(), CancellationToken.None))
                                                       .ReturnsAsync(new Response<Guid>(Guid.NewGuid()));

        public void ArrageGetBreweryById() => mockRepo.Setup(x => x.GetBreweryAsync(It.IsAny<Guid>(), CancellationToken.None))
                                                                        .ReturnsAsync(GetBreweryModel());
   

    }
}
