using Airbnb.Api.Common;
using Airbnb.Api.Models;
using Airbnb.Api.Repository;
using Airbnb.Data.Models;

namespace Airbnb.Api.Services
{
    public interface IOrderService
    {
        Task CreateOrder(RoomOrderDto roomOrderDto, long userId);
    }
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IRoomRepository _roomRepository;
        public OrderService(IOrderRepository orderRepository, IRoomRepository roomRepository)
        {
            this._orderRepository = orderRepository;
            this._roomRepository = roomRepository;
        }

        public async Task CreateOrder(RoomOrderDto roomOrderDto, long userId)
        {
            Room? room = await _roomRepository.GetById(roomOrderDto.RoomId);
            if (room == null)
            {
                throw new BadRequestException("Invalid Room Id");
            }
            if (roomOrderDto.Checkin > room.CheckoutDate) throw new BadRequestException($"Checkin must greater or equal than Checkout Date");
            if (roomOrderDto.Checkin.Date < DateTime.UtcNow.Date) throw new BadRequestException($"Checkin must greater or equal than {DateTime.UtcNow.Date.ToString()}");
            if (roomOrderDto.Checkin < room.CheckinDate) throw new BadRequestException($"Checkin must greater or equal than {room.CheckinDate.ToString()}");
            if (roomOrderDto.Checkout > room.CheckoutDate) throw new BadRequestException($"Checkout must smaller or equal than {room.CheckoutDate.ToString()}");
            if (roomOrderDto.Guests > room.Guests) throw new BadRequestException($"Guests exceed {roomOrderDto.Guests} , should not more than: {roomOrderDto.Guests}");
            //create order
            RoomOrder roomOrder = new()
            {
                Guests = roomOrderDto.Guests,
                CheckIn = roomOrderDto.Checkin,
                CheckOut = roomOrderDto.Checkout,
                RoomId = roomOrderDto.RoomId,
                UserId = userId
            };
            _orderRepository.Add(roomOrder);
            await _orderRepository.SaveChanges();
            //update availble date to room
            double checkinTimeAvailble = (roomOrderDto.Checkin.Date - room.CheckinDate.Date).TotalDays;
            double checkoutTimeAvailble = (room.CheckoutDate.Date - roomOrderDto.Checkout.Date).TotalDays;
            if(checkinTimeAvailble > 0)
            {
                room.CheckoutDate = roomOrderDto.Checkin.Date.AddDays(checkinTimeAvailble);
            }
            else if(checkoutTimeAvailble > 0)
            {
                room.CheckinDate = roomOrderDto.Checkout.Date.AddDays(-1 * checkoutTimeAvailble);
            }
            if(checkinTimeAvailble == 0 && checkoutTimeAvailble <= 0)
            {
                room.CheckinDate = roomOrderDto.Checkout.Date.AddDays(1);
            }
            await _roomRepository.SaveChanges();
        }
    }
}
