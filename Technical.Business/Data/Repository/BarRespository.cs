using Technical.Business.Data.Repository.GenericRepository;
using Technical.Business.Domain;
using Technical.Business.Domain.Entity;

namespace Technical.Business.Data.Repository
{
    public class BarRespository: GenericRepository<Bar>, IBarRespository
    {
        public BarRespository(DatabaseContext context) : base(context)
        {

        }

        public async Task<Response<Guid>> AddBarAsync(BarModel bar, CancellationToken cancellationToken)
        {
            var dataToInsert = BusinessToDB(bar);

            var dbResponse = await InsertAsync(dataToInsert, cancellationToken);
            return dbResponse.Handle(dbResponse?.Model?.BarId ?? Guid.Empty);
        }

        public async Task<Response<IEnumerable<BarModel>>> GetAllBarsAsync(CancellationToken cancellationToken)
        {
            var dbResponse = await GetAllAsync(cancellationToken);
            if (dbResponse != null && dbResponse.Model != null)
            {
                var bars = dbResponse.Model.Select(m =>
                {
                    return DBToBusiness(m);
                });
                return new Response<IEnumerable<BarModel>>(bars);
            }
            return new Response<IEnumerable<BarModel>>(null);
        }

        public async Task<Response<BarModel>> GetBarAsync(Guid barId, CancellationToken cancellationToken)
        {
            var dbResponse = await GetByIdAsync(barId, cancellationToken);
            if (dbResponse != null && dbResponse.Model != null)
            {
                return new Response<BarModel>(DBToBusiness(dbResponse.Model));
            }
            return new Response<BarModel>(null);
        }

        public async Task<Response<Guid>> UpdateBarAsync(BarModel bar, CancellationToken cancellationToken)
        {
            var dataToUpdate = BusinessToDB(bar);

            var dbResponse = await UpdateAsync(bar.BarId, dataToUpdate, cancellationToken);
            return new Response<Guid>(dbResponse?.Model == 1 ? bar.BarId : Guid.Empty);
        }

        private Bar BusinessToDB(BarModel model)
        {
            return new Bar
            {
                BarId = model.BarId,
                Name = model.Name,
            };
        }
        private BarModel DBToBusiness(Bar dbModel)
        {
            return new BarModel
            {
                BarId = dbModel.BarId,
                Name = dbModel.Name,
            };
        }
    }


    public interface IBarRespository
    {
        Task<Response<Guid>> AddBarAsync(BarModel bar, CancellationToken cancellationToken);
        Task<Response<Guid>> UpdateBarAsync(BarModel bar, CancellationToken cancellationToken);
        Task<Response<IEnumerable<BarModel>>> GetAllBarsAsync(CancellationToken cancellationToken);
        Task<Response<BarModel>> GetBarAsync(Guid barId, CancellationToken cancellationToken);

    }
}
