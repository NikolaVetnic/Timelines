using BuildingBlocks.Domain.Timelines.PhysicalPerson.Events;
using BuildingBlocks.Domain.Timelines.PhysicalPerson.ValueObjects;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;

namespace Timelines.Domain.Models;

public class PhysicalPerson : Aggregate<PhysicalPersonId>
{
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string ParentName { get; set; } = string.Empty;

    public DateOnly BirthDate { get; set; }
    public string StreetAddress { get; set; } = string.Empty; // ToDo: Expand into a proper type
    public string PersonalIdNumber { get; set; } = string.Empty;
    public string IdCardNumber { get; set; } = string.Empty;

    // ToDo: Convert into arrays to allow for multiple inputs
    public string EmailAddress { get; set; } = string.Empty; // ToDo: Expand into a proper type (work, home, etc)
    public string PhoneNumber { get; set; } = string.Empty; // ToDo: Same as above

    public string? BankAccountNumber { get; set; } = string.Empty; // ToDo: Expand into a proper type

    public string? Comment { get; set; } = string.Empty;

    public required TimelineId TimelineId { get; set; }
    public required string OwnerId { get; set; }

    #region PhysicalPerson

    public static PhysicalPerson Create(PhysicalPersonId id, string firstName, string? middleName, string lastName,
        string parentName, DateOnly birthDate, string streetAddress, string personalId, string idCardNumber,
        string emailAddress, string phoneNumber, string? bankAccountNumber, string? comment,
        string ownerId, TimelineId timelineId)
    {
        var physicalPerson = new PhysicalPerson
        {
            Id = id,
            FirstName = firstName,
            MiddleName = middleName,
            LastName = lastName,
            ParentName = parentName,
            BirthDate = birthDate,
            StreetAddress = streetAddress,
            PersonalIdNumber = personalId,
            IdCardNumber = idCardNumber,
            EmailAddress = emailAddress,
            PhoneNumber = phoneNumber,
            BankAccountNumber = bankAccountNumber,
            Comment = comment,
            TimelineId = timelineId,
            OwnerId = ownerId
        };

        physicalPerson.AddDomainEvent(new PhysicalPersonCreatedEvent(physicalPerson.Id));

        return physicalPerson;
    }

    public void Update(string firstName, string middleName, string lastName,
        string parentName, DateOnly birthDate, string streetAddress, string personalId, string idCardNumber,
        string emailAddress, string phoneNumber, string bankAccountNumber, string comment,
        string ownerId, TimelineId timelineId)
    {
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        ParentName = parentName;
        BirthDate = birthDate;
        StreetAddress = streetAddress;
        PersonalIdNumber = personalId;
        IdCardNumber = idCardNumber;
        EmailAddress = emailAddress;
        PhoneNumber = phoneNumber;
        BankAccountNumber = bankAccountNumber;
        Comment = comment;
        TimelineId = timelineId;
        OwnerId = ownerId;

        AddDomainEvent(new PhysicalPersonUpdatedEvent(Id));
    }

    #endregion
}
