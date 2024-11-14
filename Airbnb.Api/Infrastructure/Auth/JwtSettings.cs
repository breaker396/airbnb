using System.ComponentModel.DataAnnotations;

namespace Airbnb.Api.Infrastructure
{
    public class JwtSettings : IValidatableObject
    {
        public string Key { get; set; } = string.Empty;

        public int TokenExpirationInDays { get; set; }

        public int RefreshTokenExpirationInDays { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Key))
            {
                yield return new ValidationResult("No Key defined in JwtSettings config", new[] { nameof(Key) });
            }
        }
    }
}
