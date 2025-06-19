using System.Text.Json.Serialization;

namespace BuildingBlocks.Domain.Timelines.PhysicalPerson.Dtos;

public class PhysicalPersonBaseDto(
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
    string ownerId
)
{
    [JsonPropertyName("id")] public string? Id { get; set; } = id;

    [JsonPropertyName("firstName")] public string FirstName { get; set; } = firstName;
    [JsonPropertyName("middleName")] public string? MiddleName { get; set; } = middleName;
    [JsonPropertyName("lastName")] public string LastName { get; set; } = lastName;
    [JsonPropertyName("parentName")] public string ParentName { get; set; } = parentName;

    [JsonPropertyName("birthDate")] public DateOnly BirthDate { get; set; } = birthDate;
    [JsonPropertyName("streetAddress")] public string StreetAddress { get; set; } = streetAddress;
    [JsonPropertyName("personalIdNumber")] public string PersonalIdNumber { get; set; } = personalIdNumber;
    [JsonPropertyName("idCardNumber")] public string IdCardNumber { get; set; } = idCardNumber;

    [JsonPropertyName("emailAddress")] public string EmailAddress { get; set; } = emailAddress;
    [JsonPropertyName("phoneNumber")] public string PhoneNumber { get; set; } = phoneNumber;

    [JsonPropertyName("bankAccountNumber")] public string? BankAccountNumber { get; set; } = bankAccountNumber;

    [JsonPropertyName("comment")] public string? Comment { get; set; } = comment;

    [JsonPropertyName("ownerId")] public string OwnerId { get; set; } = ownerId;
}
