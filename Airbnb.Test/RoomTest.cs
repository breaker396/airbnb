using Airbnb.Api.Infrastructure;
using Airbnb.Api.Models;
using Airbnb.Api.Models.Params;
using Airbnb.Api.Repository;
using Airbnb.Api.Services;
using Airbnb.Data;
using AutoMapper;

namespace Airbnb.Test
{
    [TestFixture]
    public class RoomTest
    {
        private IRoomRepository _roomRepository;
        private IUserRepository _userRepository;
        private IMapper _mapper;
        private IRoomService _roomService;
        [OneTimeSetUp]
        public void Setup()
        {
            AirbnbDbContext airbnbDbContext = Factory.DbContextFactory.CreateNewInMemoryDb();
            _userRepository = new UserRepository(airbnbDbContext);
            _roomRepository = new RoomRepository(airbnbDbContext);
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = new Mapper(configuration);
            _roomService = new RoomService(_roomRepository, _userRepository, _mapper);
        }

        [Test]
        public async Task GetRooms()
        {
            RoomFilterParam roomFilterParam = new()
            {
                PageIndex = 1,
                PageSize = 10
            };
            var pagingData = await _roomService.GetRooms(roomFilterParam);
            Assert.IsNotNull(pagingData);
            Assert.IsTrue(pagingData.PageSize == 10);
            Assert.IsTrue((pagingData.Result.Any() && pagingData.TotalCount > 0) || (!pagingData.Result.Any() && pagingData.TotalCount == 0));
        }
        [Test]
        public async Task GetRoomExist()
        {
            RoomFilterParam roomFilterParam = new()
            {
                PageIndex = 1,
                PageSize = 10
            };
            var pagingData = await _roomService.GetRooms(roomFilterParam);
            RoomDto roomDto = pagingData.Result.First();
            var room = await _roomService.GetRoom(roomDto.Id);
            Assert.IsNotNull(room);
        }
        [Test]
        public async Task GetRoomNotExist()
        {
            var room = await _roomService.GetRoom(0);
            Assert.IsNull(room);
        }
    }
}
