using System.Text.Json.Serialization;

namespace MatchThree.Domain.Models;

public class UserEntity
{
    [JsonPropertyName("id")]
    public required long Id { get; set; }

    [JsonPropertyName("username")]
    public required string? Username { get; set; }

    [JsonPropertyName("first_name")]
    public required string FirstName { get; set; } = string.Empty;

    [JsonPropertyName("is_premium")]
    public bool IsPremium { get; set; }
    
    public string SessionHash { get; set; } = string.Empty;

    [JsonIgnore]
    public DateTime CreatedAt{ get; set; }
}