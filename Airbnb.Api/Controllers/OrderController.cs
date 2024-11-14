using Airbnb.Api.Models;
using Airbnb.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Airbnb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : AirbnbController
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            this._orderService = orderService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrder(RoomOrderDto roomOrderDto)
        {
            long userId = GetUserId();
            await _orderService.CreateOrder(roomOrderDto, userId);
            return Ok();
        }
    }
}
