using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technical.Business.Data.Repository.GenericRepository;
using Technical.Business.Domain;
using Technical.Business.Domain.Entity;

namespace Technical.Business.Data.Repository
{
    internal class BreweryRepository : GenericRepository<Brewery>, IBreweryRepository
    {
        public BreweryRepository(DatabaseContext context) : base(context)
        {

        }

        public async Task<Response<Guid>> AddBreweryAsync(BreweryModel model, CancellationToken cancellationToken)
        {
            var dataToInsert = BusinessToDB(model);

            var dbResponse = await InsertAsync(dataToInsert, cancellationToken);
            return dbResponse.Handle(dbResponse?.Model?.BreweryId ?? Guid.Empty);
        }

        public async Task<Response<IEnumerable<BreweryModel>>> GetAllBreweriesAsync(CancellationToken cancellationToken)
        {
            var dbResponse = await GetAllAsync(cancellationToken);
            if (dbResponse != null && dbResponse.Model != null)
            {
                var breweries = dbResponse.Model.Select(m =>
                {
                    return DBToBusiness(m);
                });
                return new Response<IEnumerable<BreweryModel>>(breweries);
            }
            return new Response<IEnumerable<BreweryModel>>(null);
        }

        public async Task<Response<BreweryModel>> GetBreweryAsync(Guid beweryId, CancellationToken cancellationToken)
        {
            var dbResponse = await GetByIdAsync(beweryId, cancellationToken);
            if (dbResponse != null && dbResponse.Model != null)
            {
                return new Response<BreweryModel>(DBToBusiness(dbResponse.Model));
            }
            return new Response<BreweryModel>(null);
        }

        public async Task<Response<Guid>> UpdateBreweryAsync(BreweryModel brewery, CancellationToken cancellationToken)
        {
            var dataToUpdate = BusinessToDB(brewery);

            var dbResponse = await UpdateAsync(brewery.BreweryId, dataToUpdate, cancellationToken);
            return new Response<Guid>(dbResponse?.Model == 1 ? brewery.BreweryId : Guid.Empty);

        }

        private Brewery BusinessToDB(BreweryModel model)
        {
            return new Brewery
            {
                BreweryId = model.BreweryId,
                Name = model.Name,
            };
        }
        private BreweryModel DBToBusiness(Brewery dbModel)
        {
            return new BreweryModel()
            {
                BreweryId = dbModel.BreweryId,
                Name = dbModel.Name,
            };
        }
    }
    public interface IBreweryRepository
    {
        Task<Response<Guid>> AddBreweryAsync(BreweryModel brewery, CancellationToken cancellationToken);
        Task<Response<Guid>> UpdateBreweryAsync(BreweryModel brewery, CancellationToken cancellationToken);
        Task<Response<IEnumerable<BreweryModel>>> GetAllBreweriesAsync(CancellationToken cancellationToken);
        Task<Response<BreweryModel>> GetBreweryAsync(Guid beweryId, CancellationToken cancellationToken);

    }
}
