// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

namespace Core.Api.Sdk.Contracts.Reminders.ValueObjects;

public class ReminderId
{
    public ReminderId() { }

    public ReminderId(Guid value) => Value = value;

    public Guid Value { get; init; }
}
