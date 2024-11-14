using Airbnb.Api.Repository;

namespace Airbnb.Api.Services
{
    public interface IOrderService
    {
    }
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;
        }
    }
}
