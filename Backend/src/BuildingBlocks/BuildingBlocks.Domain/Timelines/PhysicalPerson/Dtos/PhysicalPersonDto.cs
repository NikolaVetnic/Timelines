using System.Text.Json.Serialization;
using BuildingBlocks.Domain.Timelines.Timeline.Dtos;

namespace BuildingBlocks.Domain.Timelines.PhysicalPerson.Dtos;

public class PhysicalPersonDto(
    string? id,
    string firstName,
    string? middleName,
    string lastName,
    string parentName,
    DateOnly birthDate,
    string streetAddress,
    string personalIdNumber,
    string idCardNumber,
    string emailAddress,
    string phoneNumber,
    string? bankAccountNumber,
    string? comment,
    TimelineBaseDto timeline,
    string ownerId
) : PhysicalPersonBaseDto(id, firstName, middleName, lastName, parentName, birthDate, streetAddress, personalIdNumber,
    idCardNumber, emailAddress, phoneNumber, bankAccountNumber, comment, ownerId)
{
    [JsonPropertyName("timeline")] public TimelineBaseDto Timeline { get; set; } = timeline;
}
