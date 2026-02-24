using System.Globalization;
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

    public string GetPhilippineTime(DateTime dateToConvert)
    {
        DateTime philippineTime = TimeZoneInfo.ConvertTime(dateToConvert, PhTimeZone);

        return philippineTime.ToString("MMM dd, yyyy hh:mm t", CultureInfo.InvariantCulture);
    }
}
