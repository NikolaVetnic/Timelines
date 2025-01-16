using SdkNodeId = Core.Api.Sdk.Contracts.Nodes.ValueObjects.NodeId;
using SdkCreateNodeRequest = Core.Api.Sdk.Contracts.Nodes.Commands.CreateNodeRequest;
using SdkCreateNodeResponse = Core.Api.Sdk.Contracts.Nodes.Commands.CreateNodeResponse;
using SdkGetNodeByIdResponse = Core.Api.Sdk.Contracts.Nodes.Queries.GetNodeByIdResponse;

using SdkReminderId = Core.Api.Sdk.Contracts.Reminders.ValueObjects.ReminderId;
using SdkCreateReminderRequest = Core.Api.Sdk.Contracts.Reminders.Commands.CreateReminderRequest;
using SdkCreateReminderResponse = Core.Api.Sdk.Contracts.Reminders.Commands.CreateReminderResponse;
using SdkGetReminderByIdResponse = Core.Api.Sdk.Contracts.Reminders.Queries.GetReminderByIdResponse;

namespace Core.Api.Sdk.Interfaces;

public interface ICoreApiClient
{
    Task<(SdkCreateNodeResponse? Response, HttpResponseMessage RawResponse)> CreateNodeAsync(SdkCreateNodeRequest request);
    Task<(SdkCreateReminderResponse? Response, HttpResponseMessage RawResponse)> CreateReminderAsync(SdkCreateReminderRequest request);

    Task<(SdkGetNodeByIdResponse? Response, HttpResponseMessage RawResponse)> GetNodeByIdAsync(SdkNodeId nodeId);
    Task<(SdkGetReminderByIdResponse? Response, HttpResponseMessage RawResponse)> GetReminderByIdAsync(SdkReminderId reminderId);
}
