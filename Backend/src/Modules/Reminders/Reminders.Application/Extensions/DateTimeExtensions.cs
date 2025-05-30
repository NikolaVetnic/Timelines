namespace Reminders.Application.Extensions;

public static class DateTimeExtensions
{
    public static DateTime TruncateToMinute(this DateTime dt)
    {
        long ticks = dt.Ticks - (dt.Ticks % TimeSpan.TicksPerMinute);
        return new DateTime(ticks, dt.Kind);
    }
}
