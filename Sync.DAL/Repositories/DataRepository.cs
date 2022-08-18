using Sync.DAL.Entities;

namespace Sync.DAL.Repositories;

public interface IDataRepository : IGenericRepository<Data> { }

public class DataRepository : GenericRepository<Data>, IDataRepository
{
    public DataRepository(SyncDataBaseContext context) : base(context) { }
}