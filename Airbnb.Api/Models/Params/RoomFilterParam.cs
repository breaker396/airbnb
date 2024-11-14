namespace Airbnb.Api.Models.Params
{
    public class RoomFilterParam : PagingQuery
    {
        public int? Place_id { get; set; }
        public int? Adults { get; set; }
        public string? Category_tag { get; set; }
        public bool? Enable_m3_private_room { get; set; }
        public long? Photo_id { get; set; }
        public string? Search_mode { get; set; }
        public int? Guests { get; set; }
        public int? Children { get; set; }
        public DateTime? Check_in { get; set; }
        public DateTime? Check_out { get; set; }
    }
}
