using BuildingBlocks.Application.Cqrs;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Notes.Domain.Models;
using Notes.Infrastructure.Data;
using Assembly = System.Reflection.Assembly;

namespace Notes.Architecture;

public abstract class BaseTests
{
    protected static readonly Assembly DomainAssembly = typeof(Note).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(ICommand).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(NotesDbContext).Assembly;
    protected static readonly Assembly ApiAssembly = typeof(Program).Assembly;
}
