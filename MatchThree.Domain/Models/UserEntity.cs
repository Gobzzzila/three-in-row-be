using System.Text.Json.Serialization;

namespace MatchThree.Domain.Models;

public class UserEntity
{
    [JsonPropertyName("id")]
    public required long Id { get; set; }

    [JsonPropertyName("username")]
    public required string? Username { get; set; }

    [JsonPropertyName("first_name")]
    public required  string FirstName { get; set; } = string.Empty;

    [JsonPropertyName("is_premium")]
    public required  bool IsPremium { get; set; }

    [JsonIgnore]
    public DateTime CreatedAt{ get; set; }
}