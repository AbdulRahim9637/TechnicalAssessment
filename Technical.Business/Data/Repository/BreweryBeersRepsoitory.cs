using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technical.Business.Domain.Entity;
using Technical.Business.Domain;
using Technical.Business.Data.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Technical.Business.Data.Repository
{
    public class BreweryBeersRepsoitory : GenericRepository<BreweryBeerMapping>, IBreweryBeersRepsoitory
    {
        private DatabaseContext _databaseContext;
        public BreweryBeersRepsoitory(DatabaseContext context) : base(context)
        {
            _databaseContext = context;
        }
        public async Task<Response<Guid>> AddBreweryWithBeerAsync(Guid beweryId, Guid beerId, CancellationToken cancellationToken)
        {
            var breweryWithBeer = new BreweryBeerMapping() { Id = Guid.NewGuid(), BeerId = beerId, BreweryId = beweryId };
            var dbResponse = await InsertAsync(breweryWithBeer, cancellationToken);
            return dbResponse.Handle(dbResponse?.Model?.Id ?? Guid.Empty);
        }

        public async Task<Response<IEnumerable<BreweryWithBeersModel>>> GetAllBrewariesWithBeersAsync(CancellationToken cancellationToken)
        {

            var response = _databaseContext.BrewersBeersMapping.AsNoTracking().GroupBy(bw => bw.Brewery).
                Select(s => new BreweryWithBeersModel()
                {
                    Brewery = new BreweryModel() { BreweryId = s.Key.BreweryId, Name = s.Key.Name },
                    Beers = s.Select(se => new BeerModel()
                    {
                        BeerId = se.BeerId,
                        Name = se.Beer.Name,
                        PercentageAlcoholByVolume = se.Beer.PercentageAlcoholByVolume
                    }).ToList()

                }).ToList();

            return new Response<IEnumerable<BreweryWithBeersModel>>(response);

        }

        public async Task<Response<BreweryWithBeersModel>> GetBreweryWithBeersAsync(Guid beweryId, CancellationToken cancellationToken)
        {
            var association = _databaseContext.BrewersBeersMapping.AsNoTracking().Where(f => f.BreweryId == beweryId).
                 GroupBy(g => g.Brewery).Select(s => new BreweryWithBeersModel()
                 {
                     Brewery = new BreweryModel() { BreweryId = s.Key.BreweryId, Name = s.Key.Name },
                     Beers = s.Select(se => new BeerModel() { BeerId = se.BeerId, Name = se.Beer.Name, PercentageAlcoholByVolume = se.Beer.PercentageAlcoholByVolume }).ToList()

                 }).FirstOrDefault();
            return new Response<BreweryWithBeersModel>(association);
        }
    }

    public interface IBreweryBeersRepsoitory
    {
        Task<Response<Guid>> AddBreweryWithBeerAsync(Guid beweryId, Guid beerId, CancellationToken cancellationToken);
        Task<Response<IEnumerable<BreweryWithBeersModel>>> GetAllBrewariesWithBeersAsync(CancellationToken cancellationToken);
        Task<Response<BreweryWithBeersModel>> GetBreweryWithBeersAsync(Guid beweryId, CancellationToken cancellationToken);

    }
}
