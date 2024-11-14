using Airbnb.Api.Infrastructure;
using Airbnb.Api.Models;
using Airbnb.Api.Repository;
using Airbnb.Api.Services;
using Airbnb.Data;
using AutoMapper;

namespace Airbnb.Test
{
    [TestFixture]
    public class UserTest
    {
        private IUserRepository _userRepository;
        private IUserService _userService;
        private IMapper _mapper;
        [OneTimeSetUp]
        public void Setup()
        {
            AirbnbDbContext airbnbDbContext = Factory.DbContextFactory.CreateNewInMemoryDb();
            _userRepository = new UserRepository(airbnbDbContext);
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = new Mapper(configuration);
            _userService = new UserService(_userRepository, _mapper);
        }

        [Test]
        public async Task Test_Regiter()
        {
            UserRegisterDto userRegisterDto = new UserRegisterDto() 
            {
                UserName = "RamdomName",
                Password = "password"
            };
            var user = await _userService.Register(userRegisterDto);
            Assert.IsNotNull(user);
        }
        [Test]
        public async Task Test_Login()
        {
            UserRegisterDto userRegisterDto = new UserRegisterDto()
            {
                UserName = "Seller",
                Password = "test1"
            };
            var user = await _userService.Login(userRegisterDto);
            Assert.IsNotNull(user);
        }
    }
}
