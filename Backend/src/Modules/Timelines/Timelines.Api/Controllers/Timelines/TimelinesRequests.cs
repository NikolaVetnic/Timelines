namespace Timelines.Api.Controllers.Timelines;

public class CreateTimelineRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
}

public class CreateTimelineWithTemplateRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
}

public class UpdateTimelineRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
}
