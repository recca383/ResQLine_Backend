using SharedKernel;

namespace Infrastructure.Time;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    private static readonly TimeZoneInfo PhTimeZone =
        TimeZoneInfo.FindSystemTimeZoneById("Asia/Manila");

    public DateTime UtcNow => DateTime.UtcNow;

    public DateTime NowInPhilippines => TimeZoneInfo.ConvertTimeFromUtc(
        DateTime.Now,
        PhTimeZone
        );
}
