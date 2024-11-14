using Airbnb.Api.Infrastructure;
using Airbnb.Api.Models;
using Airbnb.Api.Models.Params;
using Airbnb.Test.Factory;
using AutoMapper;
using System.Net;
using System.Net.NetworkInformation;
using System.Text.Json;

namespace Airbnb.Test
{
    [TestFixture]
    public class IntegrationTests
    {
        private AirbnbWebApplicationFactory<Airbnb.Api.Program> _factory;
        private HttpClient _client;
        private IMapper _mapper;


        [OneTimeSetUp]
        public void SetUp()
        {
            _factory = new AirbnbWebApplicationFactory<Airbnb.Api.Program>();
            _client = _factory.CreateClient();
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = new Mapper(configuration);
        }

        [Test]
        public async Task GetRooms()
        {
            var request = "/api/Room/";
            var response = await _client.GetAsync(request);
            string content = await response.Content.ReadAsStringAsync();
            Paging<RoomDto>? paging = JsonSerializer.Deserialize<Paging<RoomDto>>(content);
            Assert.IsNotNull(paging);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
        [Test]
        public async Task GetRoom()
        {
            var request = "/api/Room/";
            var response = await _client.GetAsync(request);
            string content = await response.Content.ReadAsStringAsync();
            Paging<RoomDto>? paging = JsonSerializer.Deserialize<Paging<RoomDto>>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            Assert.IsNotNull(paging);
            if (paging != null)
            {
                long id = paging.Result.First().Id;
                request = $"/api/Room/{id}";
                response = await _client.GetAsync(request);
            }
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }


    }
}
