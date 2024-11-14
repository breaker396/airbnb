using Airbnb.Api.Models.Params;
using Airbnb.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Airbnb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        public RoomController(IRoomService roomService)
        {
            this._roomService = roomService;
        }
        [HttpGet]
        public async Task<IActionResult> GetRooms([FromQuery] RoomFilterParam roomFilterParam)
        {
            var rs = await _roomService.GetRooms(roomFilterParam);
            return Ok(rs);
        }
    }
}
