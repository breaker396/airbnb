using Airbnb.Data;

namespace Airbnb.Api.Repository
{
    public interface IOrderRepository
    {
    }
    public class OrderRepository : BaseRepository, IOrderRepository
    {
        public OrderRepository(AirbnbDbContext airbnbDbContext) : base(airbnbDbContext)
        {
        }
    }
}
