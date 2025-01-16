using Core.Api.Sdk.Interfaces;
using Mapster;
using System.Net.Http.Json;
using ApiCreateNoteRequest = Notes.Api.Endpoints.Notes.CreateNoteRequest;
using ApiCreateNoteResponse = Notes.Api.Endpoints.Notes.CreateNoteResponse;
using SdkCreateNoteRequest = Core.Api.Sdk.Contracts.Notes.Commands.CreateNoteRequest;
using SdkCreateNoteResponse = Core.Api.Sdk.Contracts.Notes.Commands.CreateNoteResponse;

namespace Core.Api.Sdk;

public partial class CoreApiClient : ICoreApiClient
{
    public async Task<(SdkCreateNoteResponse? Response, HttpResponseMessage RawResponse)> CreateNoteAsync(SdkCreateNoteRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var apiRequest = request.Adapt<ApiCreateNoteRequest>();
        var response = await httpClient.PostAsJsonAsync("/notes", apiRequest);

        if (!response.IsSuccessStatusCode)
            return (null, response);

        var apiResponse = await response.Content.ReadFromJsonAsync<ApiCreateNoteResponse>();
        var sdkCreateNoteResponse = apiResponse.Adapt<SdkCreateNoteResponse>();

        return (sdkCreateNoteResponse, response);
    }
}
