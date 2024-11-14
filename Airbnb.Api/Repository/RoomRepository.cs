using Airbnb.Api.Models.Params;
using Airbnb.Data;
using Airbnb.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.Api.Repository
{
    public interface IRoomRepository
    {
        public IQueryable<Room> GetRooms(RoomFilterParam roomFilterParam);
        public Task<Room?> GetById(long id, bool includeChild = false);
        public Task<Category?> GetCategoryById(int id);
        public Task<Province?> GetProvinceById(int id);
        public Task<Currency?> GetCurrencyById(int id);
    }
    public class RoomRepository : BaseRepository, IRoomRepository
    {
        public RoomRepository(AirbnbDbContext airbnbDbContext) : base(airbnbDbContext) { }

        public Task<Room?> GetById(long id, bool includeChild = false)
        {
            var query = _airbnbDbContext.Rooms.Where(room => room.Id == id);
            if (!includeChild) return query.FirstOrDefaultAsync();
            return query
                .Include(x => x.Category)
                .Include(x => x.User)
                .Include(x => x.Province)
                .Include(x => x.Currency)
                .FirstOrDefaultAsync(room => room.Id == id);
        }

        public Task<Category?> GetCategoryById(int id)
        {
            return _airbnbDbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task<Currency?> GetCurrencyById(int id)
        {
            return _airbnbDbContext.Currencies.FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task<Province?> GetProvinceById(int id)
        {
            return _airbnbDbContext.Provinces.FirstOrDefaultAsync(p => p.Id == id);
        }

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
            if (!string.IsNullOrEmpty(roomFilterParam.OrderBy))
            {
                switch (roomFilterParam.OrderBy)
                {
                    case "Rating":
                        query = roomFilterParam.OrderByDesc ? query.OrderByDescending(r => r.Rating).AsQueryable() : query.OrderBy(r => r.Rating).AsQueryable();
                        break;
                    case "Price":
                        query = roomFilterParam.OrderByDesc ? query.OrderByDescending(r => r.Price).AsQueryable() : query.OrderBy(r => r.Price).AsQueryable();
                        break;
                    case "Guests":
                        query = roomFilterParam.OrderByDesc ? query.OrderByDescending(r => r.Guests).AsQueryable() : query.OrderBy(r => r.Guests).AsQueryable();
                        break;
                }
            }
            return query;
        }
    }
}
