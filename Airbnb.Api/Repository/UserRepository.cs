using Airbnb.Data;
using Airbnb.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.Api.Repository
{
    public interface IUserRepository : IBaseRepository
    {
        Task<User?> GetById(long Id);
        Task<User?> GetByUsernameAndPassword(string username, string password);
        Task<bool> IsExistUsername(string username);
        void Add(User user);
    }
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(AirbnbDbContext airbnbDbContext) : base(airbnbDbContext) { }

        public void Add(User user)
        {
            this._airbnbDbContext.Users.Add(user);
        }

        public Task<User?> GetById(long Id)
        {
            return _airbnbDbContext.Users.FirstOrDefaultAsync(u => u.Id == Id);
        }

        public Task<User?> GetByUsernameAndPassword(string username, string password)
        {
            return _airbnbDbContext.Users.FirstOrDefaultAsync(u => u.Name == username && u.Password == password);
        }

        public Task<bool> IsExistUsername(string username)
        {
            return _airbnbDbContext.Users.AnyAsync(u => u.Name == username);
        }
    }
}
