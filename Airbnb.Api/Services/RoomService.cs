using Airbnb.Api.Models;
using Airbnb.Api.Models.Params;
using Airbnb.Api.Repository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.Api.Services
{
    public interface IRoomService
    {
        Task<Paging<RoomDto>> GetRooms(RoomFilterParam roomFilterParam);
    }
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;
        public RoomService(IRoomRepository roomRepository, IMapper mapper)
        {
            this._roomRepository = roomRepository;
            this._mapper = mapper;
        }
        public async Task<Paging<RoomDto>> GetRooms(RoomFilterParam roomFilterParam)
        {
            Paging<RoomDto> paging = new Paging<RoomDto>(roomFilterParam.PageIndex, roomFilterParam.PageSize);
            var query = _roomRepository.GetRooms(roomFilterParam);
            int totalRows = await query.CountAsync();
            paging.TotalCount = totalRows;
            var rooms = await query.Skip((roomFilterParam.PageIndex - 1) * roomFilterParam.PageSize).Take(roomFilterParam.PageSize).ToListAsync();
            var roomDtos = _mapper.Map<List<RoomDto>>(rooms);
            paging.Result = roomDtos;
            return paging;
        }
    }
}
