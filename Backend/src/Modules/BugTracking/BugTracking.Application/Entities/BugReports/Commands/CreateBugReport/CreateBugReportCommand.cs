using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Domain.BugTracking.BugReport.ValueObject;
using FluentValidation;

namespace BugTracking.Application.Entities.BugReports.Commands.CreateBugReport;

// ReSharper disable once ClassNeverInstantiated.Global
public record CreateBugReportCommand : ICommand<CreateBugReportResult>
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string ReporterName { get; set; }
}

// ReSharper disable once NotAccessedPositionalProperty.Global
public record CreateBugReportResult(BugReportId Id);

public class CreateReminderCommandValidator : AbstractValidator<CreateBugReportCommand>
{
    public CreateReminderCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(2000).WithMessage("Description must not exceed 2000 characters.");
        
        RuleFor(x => x.ReporterName)
            .NotEmpty().WithMessage("Reporter's name is required.")
            .MaximumLength(100).WithMessage("Reporter's name must not exceed 100 characters.");
    }
}