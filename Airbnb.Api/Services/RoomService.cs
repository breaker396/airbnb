using Airbnb.Api.Models;
using Airbnb.Api.Models.Params;
using Airbnb.Api.Repository;
using Airbnb.Data.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.Api.Services
{
    public interface IRoomService
    {
        Task<Paging<RoomDto>> GetRooms(RoomFilterParam roomFilterParam);
        Task<RoomDto?> GetRoom(long roomId);
    }
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public RoomService(IRoomRepository roomRepository, IUserRepository userRepository, IMapper mapper)
        {
            this._roomRepository = roomRepository;
            this._userRepository = userRepository;
            this._mapper = mapper;
        }

        public async Task<Paging<RoomDto>> GetRooms(RoomFilterParam roomFilterParam)
        {
            Paging<RoomDto> paging = new Paging<RoomDto>(roomFilterParam.PageIndex, roomFilterParam.PageSize);
            var query = _roomRepository.GetRooms(roomFilterParam);
            int totalRows = await query.CountAsync();
            paging.TotalCount = totalRows;
            var rooms = await query.Skip((roomFilterParam.PageIndex - 1) * roomFilterParam.PageSize).Take(roomFilterParam.PageSize).ToListAsync();
            foreach(var room in rooms)
            {
                if(room.User == null)
                {
                    User? user = await _userRepository.GetById(room.UserId);
                    if(user != null) room.User = user;
                }
                if (room.Category == null)
                {
                    Category? category = await _roomRepository.GetCategoryById(room.CategoryId);
                    if (category != null) room.Category = category;
                }
                if (room.Province == null)
                {
                    Province? province = await _roomRepository.GetProvinceById(room.ProvinceId);
                    if (province != null) room.Province = province;
                }
                if (room.Currency == null)
                {
                    Currency? currency = await _roomRepository.GetCurrencyById(room.CurrencyId);
                    if (currency != null) room.Currency = currency;
                }
            }
            var roomDtos = _mapper.Map<List<RoomDto>>(rooms);
            paging.Result = roomDtos;
            return paging;
        }

        public async Task<RoomDto?> GetRoom(long roomId)
        {
            var room = await _roomRepository.GetById(roomId, true);
            if (room != null)
            {
                var userCreated = await _userRepository.GetById(room.CreatedBy);
                RoomDto roomDto = _mapper.Map<RoomDto>(room);
                roomDto.CreatedByName = userCreated?.Name ?? string.Empty;
                return roomDto;
            }
            return null;
        }
    }
}
