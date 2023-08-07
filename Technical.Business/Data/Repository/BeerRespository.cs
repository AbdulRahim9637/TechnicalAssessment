using Microsoft.EntityFrameworkCore;
using Technical.Business.Data.Repository.GenericRepository;
using Technical.Business.Domain;
using Technical.Business.Domain.Entity;

namespace Technical.Business.Data.Repository
{
    public class BeerRespository : GenericRepository<Beer>, IBeerRespository
    {
        private DatabaseContext _databaseContext;
        public BeerRespository(DatabaseContext context) : base(context)
        {
            _databaseContext=context;
        }

        public async Task<Response<Guid>> AddBeerAsync(BeerModel model, CancellationToken cancellationToken)
        {
            var dataToInsert = BusinessToDB(model);

            var dbResponse = await InsertAsync(dataToInsert, cancellationToken);
            return new Response<Guid>(dbResponse?.Model?.BeerId ?? Guid.Empty);
        }

        public async Task<Response<IEnumerable<BeerModel>>> GetAllBeersAsync(decimal? GtAlcohol, decimal? LtAlcohol,CancellationToken cancellationToken)
        {
            //todo will move to generic repository 
            IQueryable<Beer> queryable = _databaseContext.Set<Beer>();


            if (LtAlcohol != null )
                queryable = queryable.Where(q => q.PercentageAlcoholByVolume < LtAlcohol.Value);
            if (GtAlcohol != null )
                queryable= queryable.Where(q => q.PercentageAlcoholByVolume > GtAlcohol.Value);

            var dbResponse = queryable.ToList();
           
            if (dbResponse != null )
            {
                var beers = dbResponse.Select(m =>
                {
                    return DBToBusiness(m);
                });
                return new Response<IEnumerable<BeerModel>>(beers);
            }
            return new Response<IEnumerable<BeerModel>>(null);
        }

        public async Task<Response<BeerModel>> GetBeerAsync(Guid beerId, CancellationToken cancellationToken)
        {
            var dbResponse = await GetByIdAsync(beerId, cancellationToken);
            if (dbResponse != null && dbResponse.Model != null)
            {
                return new Response<BeerModel>(DBToBusiness(dbResponse.Model));
            }
            return new Response<BeerModel>(null);
        }

        public async Task<Response<Guid>> UpdateBeerAsync(BeerModel beer, CancellationToken cancellationToken)
        {
            var dataToUpdate = BusinessToDB(beer);

            var dbResponse = await UpdateAsync(beer.BeerId, dataToUpdate, cancellationToken);
            return new Response<Guid>(dbResponse?.Model == 1 ? beer.BeerId : Guid.Empty);
        }

        private Beer BusinessToDB(BeerModel model)
        {
            return new Beer
            {
                BeerId = model.BeerId,
                Name = model.Name,
                PercentageAlcoholByVolume = model.PercentageAlcoholByVolume
            };
        }
        private BeerModel DBToBusiness(Beer dbModel)
        {
            return new BeerModel
            {
                BeerId = dbModel.BeerId,
                Name = dbModel.Name,
                PercentageAlcoholByVolume = dbModel.PercentageAlcoholByVolume
            };
        }
    }
    public interface IBeerRespository
    {
        Task<Response<Guid>> AddBeerAsync(BeerModel beer, CancellationToken cancellationToken);
        Task<Response<Guid>> UpdateBeerAsync(BeerModel beer, CancellationToken cancellationToken);
        Task<Response<IEnumerable<BeerModel>>> GetAllBeersAsync(decimal? GtAlcohol,decimal? LtAlcohol, CancellationToken cancellationToken);
        Task<Response<BeerModel>> GetBeerAsync(Guid beerId, CancellationToken cancellationToken);

    }
}
