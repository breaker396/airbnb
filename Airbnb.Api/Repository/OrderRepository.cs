using Airbnb.Data;
using Airbnb.Data.Models;

namespace Airbnb.Api.Repository
{
    public interface IOrderRepository : IBaseRepository
    {
        void Add(RoomOrder order);
    }
    public class OrderRepository : BaseRepository, IOrderRepository
    {
        public OrderRepository(AirbnbDbContext airbnbDbContext) : base(airbnbDbContext)
        {
        }

        public void Add(RoomOrder order)
        {
            _airbnbDbContext.RoomOrders.Add(order);
        }
    }
}
