namespace Airbnb.Api.Models
{
    public class UserDto
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public List<RoomDto> Rooms { get; set; } = new();
    }
}
