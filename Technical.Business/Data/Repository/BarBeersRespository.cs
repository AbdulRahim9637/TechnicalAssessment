using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technical.Business.Domain.Entity;
using Technical.Business.Domain;
using Technical.Business.Data.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace Technical.Business.Data.Repository
{
    public class BarBeersRespository : GenericRepository<BeerBarMapping>, IBarBeersRepsoitory
    {
        private DatabaseContext _databaseContext;
        public BarBeersRespository(DatabaseContext context) : base(context)
        {
            _databaseContext = context;
        }

        public async Task<Response<Guid>> AddBarWithBeerAsync(Guid barId, Guid beerId, CancellationToken cancellationToken)
        {
            var barWithBeer = new BeerBarMapping() { Id = Guid.NewGuid(), BeerId = beerId, BarId = barId };
            var dbResponse = await InsertAsync(barWithBeer, cancellationToken);
            return dbResponse.Handle(dbResponse?.Model?.Id ?? Guid.Empty);
        }

        public async Task<Response<IEnumerable<BarWithBeersModel>>> GetAllBarsWithBeersAsync(CancellationToken cancellationToken)
        {
            var response = _databaseContext.BeersBarsMapping.AsNoTracking().GroupBy(bw => bw.Bar).
              Select(s => new BarWithBeersModel()
              {
                  Bar = new BarModel() { BarId = s.Key.BarId, Name = s.Key.Name },
                  Beers = s.Select(se => new BeerModel()
                  {
                      BeerId = se.BeerId,
                      Name = se.Beer.Name,
                      PercentageAlcoholByVolume = se.Beer.PercentageAlcoholByVolume
                  }).ToList()

              }).AsEnumerable();

            return new Response<IEnumerable<BarWithBeersModel>>(response);

        }

        public async Task<Response<BarWithBeersModel>> GetBarWithBeersAsync(Guid barId, CancellationToken cancellationToken)
        {
            var association = _databaseContext.BeersBarsMapping.AsNoTracking().Where(f => f.BarId == barId).
                  GroupBy(g => g.Bar).Select(s => new BarWithBeersModel()
                  {
                      Bar = new BarModel() { BarId = s.Key.BarId, Name = s.Key.Name },
                      Beers = s.Select(se => new BeerModel() { BeerId = se.BeerId, Name = se.Beer.Name, PercentageAlcoholByVolume = se.Beer.PercentageAlcoholByVolume }).ToList()

                  }).FirstOrDefault();
            return new Response<BarWithBeersModel>(association);
        }
    }

    public interface IBarBeersRepsoitory
    {
        Task<Response<Guid>> AddBarWithBeerAsync(Guid barId, Guid beerId, CancellationToken cancellationToken);
        Task<Response<IEnumerable<BarWithBeersModel>>> GetAllBarsWithBeersAsync(CancellationToken cancellationToken);
        Task<Response<BarWithBeersModel>> GetBarWithBeersAsync(Guid barId, CancellationToken cancellationToken);

    }
}
