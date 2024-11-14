using System.ComponentModel.DataAnnotations;

namespace Airbnb.Api.Models
{
    public class UserRegisterDto
    {
        [Required]
        [Length(1, 60)]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [Length(1, 20)]
        public string Password { get; set; } = string.Empty;
    }
}
