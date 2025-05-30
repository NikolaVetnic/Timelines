using BuildingBlocks.Application.Cqrs;
using Reminders.Domain.Models;
using Reminders.Infrastructure.Data;
using Assembly = System.Reflection.Assembly;

namespace Reminders.Architecture;

public abstract class BaseTests
{
    protected static readonly Assembly DomainAssembly = typeof(Reminder).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(ICommand).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(RemindersDbContext).Assembly;
    protected static readonly Assembly ApiAssembly = typeof(Program).Assembly;
}
