using System.Net.Http.Json;
using Mapster;
using ApiCreateReminderRequest = Reminders.Api.Endpoints.Reminders.CreateReminderRequest;
using ApiCreateReminderResponse = Reminders.Api.Endpoints.Reminders.CreateReminderResponse;
using ApiGetReminderByIdResponse = Reminders.Api.Endpoints.Reminders.GetReminderByIdResponse;
using SdkReminderId = Core.Api.Sdk.Contracts.Reminders.ValueObjects.ReminderId;
using SdkCreateReminderRequest = Core.Api.Sdk.Contracts.Reminders.Commands.CreateReminderRequest;
using SdkCreateReminderResponse = Core.Api.Sdk.Contracts.Reminders.Commands.CreateReminderResponse;
using SdkGetReminderByIdResponse = Core.Api.Sdk.Contracts.Reminders.Queries.GetReminderByIdResponse;

namespace Core.Api.Sdk;

public partial class CoreApiClient
{
    public async Task<(SdkCreateReminderResponse? Response, HttpResponseMessage RawResponse)> CreateReminderAsync(
        SdkCreateReminderRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var apiRequest = request.Adapt<ApiCreateReminderRequest>();
        var response = await httpClient.PostAsJsonAsync("/reminders", apiRequest);

        if (!response.IsSuccessStatusCode)
            return (null, response);

        var apiResponse = await response.Content.ReadFromJsonAsync<ApiCreateReminderResponse>();
        var sdkCreateReminderResponse = apiResponse.Adapt<SdkCreateReminderResponse>();

        return (sdkCreateReminderResponse, response);
    }

    public async Task<(SdkGetReminderByIdResponse? Response, HttpResponseMessage RawResponse)> GetReminderByIdAsync(
        SdkReminderId reminderId)
    {
        if (reminderId == null)
            throw new ArgumentNullException(nameof(reminderId));

        var response = await httpClient.GetAsync($"/reminders/{reminderId.Value}");

        if (!response.IsSuccessStatusCode)
            return (null, response);

        var apiReminder = await response.Content.ReadFromJsonAsync<ApiGetReminderByIdResponse>();
        var sdkReminder = apiReminder.Adapt<SdkGetReminderByIdResponse>();

        return (sdkReminder, response);
    }
}
