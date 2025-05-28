using System;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;

namespace Timelines.Api.Controllers.PhysicalPersons;

public class CreatePhysicalPersonRequest
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string ParentName { get; set; }
    public DateOnly BirthDate { get; set; }
    public string StreetAddress { get; set; }
    public string PersonalIdNumber { get; set; }
    public string IdCardNumber { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public string BankAccountNumber { get; set; }
    public string Comment { get; set; }
    public TimelineId TimelineId { get; set; }
}

public class UpdatePhysicalPersonRequest
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string ParentName { get; set; }
    public DateOnly BirthDate { get; set; }
    public string StreetAddress { get; set; }
    public string PersonalIdNumber { get; set; }
    public string IdCardNumber { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public string BankAccountNumber { get; set; }
    public string Comment { get; set; }
}
