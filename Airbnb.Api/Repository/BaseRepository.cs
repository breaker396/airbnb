using Airbnb.Data;

namespace Airbnb.Api.Repository
{
    public interface IBaseRepository
    {
        Task<int> SaveChanges();
    }
    public abstract class BaseRepository : IBaseRepository
    {
        protected readonly AirbnbDbContext _airbnbDbContext;
        protected BaseRepository(AirbnbDbContext airbnbDbContext)
        {
            _airbnbDbContext = airbnbDbContext;
        }
        public virtual Task<int> SaveChanges()
        {
            return _airbnbDbContext.SaveChangesAsync();
        }
    }
}
