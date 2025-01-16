using SdkReminderId = Core.Api.Sdk.Contracts.Reminders.ValueObjects.ReminderId;
using SdkReminderDto = Core.Api.Sdk.Contracts.Reminders.Dtos.ReminderDto;
using SdkCreateReminderRequest = Core.Api.Sdk.Contracts.Reminders.Commands.CreateReminderRequest;

// ReSharper disable ConvertToPrimaryConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable Reqnroll.MethodNameMismatchPattern

namespace Reminders.Integration.StepDefinitions;

[Binding]
public class RemindersApiStepDefinitions
{
    private readonly ICoreApiClient _apiApiClient;
    private HttpResponseMessage? _response;

    private SdkReminderDto? _reminderDto;
    private SdkReminderId? _persistedReminderId;

    public RemindersApiStepDefinitions(ICoreApiClient apiClient)
    {
        _apiApiClient = apiClient;
    }

    #region When

    [When("a POST request is sent to the /Reminders endpoint with a valid payload")]
    public async Task WhenAPostRequestIsSentToTheRemindersEndpoint()
    {
        var (response, rawResponse) =
            await _apiApiClient.CreateReminderAsync(new SdkCreateReminderRequest
            {
                Reminder = new SdkReminderDto
                {
                    Id = null,
                    Title = "Submit Annual Tax Return",
                    Description = "Prepare and submit the annual tax return before the due date to avoid penalties.",
                    DueDateTime = DateTime.Parse("2025-03-15T14:24:06.919459Z", null, DateTimeStyles.RoundtripKind),
                    Priority = 3,
                    NotificationTime = DateTime.Parse("2025-02-15T10:00:00Z", null, DateTimeStyles.RoundtripKind),
                    Status =  "Canceled"
                }
            });

        (_response = rawResponse).EnsureSuccessStatusCode();
        rawResponse.EnsureSuccessStatusCode();

        (_persistedReminderId = response!.Id).Should().NotBeNull();
    }

    #endregion

    #region Then

    [Then(@"the response status code is (\d{3}) \((.+)\)")]
    public void ThenTheResponseStatusCodeIs(int expectedStatusCode, string description)
    {
        if (_response != null)
            ((int)_response.StatusCode).Should().Be(expectedStatusCode);
    }

    [Then("the Reminder is created")]
    public async Task ThenTheReminderIsCreated()
    {
        var (response, rawResponse) = await _apiApiClient.GetReminderByIdAsync(_persistedReminderId!);

        rawResponse.EnsureSuccessStatusCode();
        var reminderDto = response!.ReminderDto;

        reminderDto?.Title.Should().Be("Submit Annual Tax Return");
        reminderDto?.Description.Should().Be("Prepare and submit the annual tax return before the due date to avoid penalties.");
        reminderDto?.DueDateTime.Should().Be(DateTime.Parse("2025-03-15T14:24:06.919459Z", null, DateTimeStyles.RoundtripKind));
        reminderDto?.Priority.Should().Be(3);
        reminderDto?.NotificationTime.Should().Be(DateTime.Parse("2025-02-15T10:00:00Z", null, DateTimeStyles.RoundtripKind));
        reminderDto?.Status.Should().Be("Canceled");
    }

    #endregion
}
