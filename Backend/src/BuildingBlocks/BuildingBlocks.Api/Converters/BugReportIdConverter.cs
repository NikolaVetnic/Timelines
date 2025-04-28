using BuildingBlocks.Domain.BugTracking.BugReport.ValueObject;

namespace BuildingBlocks.Api.Converters;

public class BugReportIdConverter : IRegister
{
    public void Register(TypeAdapterConfig config) =>
        config.NewConfig<BugReportId, BugReportId>().ConstructUsing(src => BugReportId.Of(src.Value));
}
