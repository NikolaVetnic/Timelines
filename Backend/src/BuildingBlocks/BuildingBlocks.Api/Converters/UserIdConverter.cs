using BuildingBlocks.Domain.Users.User.ValueObjects;

namespace BuildingBlocks.Api.Converters;

public class UserIdConverter
{
    public void Register(TypeAdapterConfig config) =>
        config.NewConfig<UserId, UserId>().ConstructUsing(src => UserId.Of(src.Value));
}
