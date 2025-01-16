using SdkReminderId = Core.Api.Sdk.Contracts.Reminders.ValueObjects.ReminderId;
using SdkCreateReminderRequest = Core.Api.Sdk.Contracts.Reminders.Commands.CreateReminderRequest;
using SdkCreateReminderResponse = Core.Api.Sdk.Contracts.Reminders.Commands.CreateReminderResponse;
using SdkGetReminderByIdResponse = Core.Api.Sdk.Contracts.Reminders.Queries.GetReminderByIdResponse;

namespace Core.Api.Sdk.Interfaces;

public partial interface ICoreApiClient
{
    Task<(SdkCreateReminderResponse? Response, HttpResponseMessage RawResponse)> CreateReminderAsync(SdkCreateReminderRequest request);
    Task<(SdkGetReminderByIdResponse? Response, HttpResponseMessage RawResponse)> GetReminderByIdAsync(SdkReminderId reminderId);
}
