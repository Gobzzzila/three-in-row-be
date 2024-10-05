namespace MatchThree.API.Models.User;

public class CreateUserRequestDto : MainUserInfoDto
{
    public long Id { get; init; }
    public long? ReferrerId { get; init; }
}