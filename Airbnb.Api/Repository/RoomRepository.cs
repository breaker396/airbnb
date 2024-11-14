using Airbnb.Api.Models.Params;
using Airbnb.Data;
using Airbnb.Data.Models;

namespace Airbnb.Api.Repository
{
    public interface IRoomRepository
    {
        public IQueryable<Room> GetRooms(RoomFilterParam roomFilterParam);
    }
    public class RoomRepository : BaseRepository, IRoomRepository
    {
        public RoomRepository(AirbnbDbContext airbnbDbContext) : base(airbnbDbContext) { }

        public IQueryable<Room> GetRooms(RoomFilterParam roomFilterParam)
        {
            var query = this._airbnbDbContext.Rooms.AsQueryable();
            if (roomFilterParam.Place_id.HasValue)
            {
                query = query.Where(x => x.ProvinceId == roomFilterParam.Place_id.Value);
            }
            if (roomFilterParam.Adults.HasValue)
            {
                query = query.Where(x => x.Adults >= roomFilterParam.Adults.Value);
            }
            if (roomFilterParam.Check_in.HasValue)
            {
                query = query.Where(x => x.CheckinDate >= roomFilterParam.Check_in.Value);
            }
            if (roomFilterParam.Check_out.HasValue)
            {
                query = query.Where(x => x.CheckinDate <= roomFilterParam.Check_out.Value);
            }
            if (roomFilterParam.Guests.HasValue)
            {
                query = query.Where(x => x.Guests >= roomFilterParam.Guests.Value);
            }
            return query;
        }
    }
}
