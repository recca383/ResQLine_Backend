using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI;
using Domain.Reports;
using SharedKernel;

namespace Application.Reports;
internal static class MessageTemplates
{
    private const string SUMMARY_REPORT_FOR_RESPONDERS =
        """
                RESQLINE EMERGENCY REPORT

        Incident: {incident}

        AI Image Analysis:
        {ListOfTags}

        Reporter Notes:
        "{description}"

        Location:
        {location}

        Exact Coordinates:
        Latitude: {latitude}
        Longitude: {longitude}


        Timestamp:
        {Timestamp}
        ResqLine Automated Dispatch
        """;

    public const string THANK_FOR_REPORTING =
        """
        Thank you for your report. Emergency responders have been notified and are on their way to assist you.

        """;

    public static string CreateSummaryReport(Report report, IDateTimeProvider dateTimeProvider, string tags)
    {
        string message = MessageTemplates.SUMMARY_REPORT_FOR_RESPONDERS
                .Replace("{incident}", report.GetCategoryString())
                .Replace("{description}", string.IsNullOrWhiteSpace(report.Description) ?
                                        "No additional notes provided."
                                        : report.Description)
                .Replace("{ListOfTags}", tags)
                .Replace("{latitude}", report.ReportedAt.Latitude.ToString(CultureInfo.InvariantCulture))
                .Replace("{longitude}", report.ReportedAt.Longitude.ToString(CultureInfo.InvariantCulture))
                .Replace("{Timestamp}", dateTimeProvider.GetPhilippineTime(report.DateCreated))
                .Replace("{location}", report.ReportedAt.ReverseGeoCode);

        return message;
    }

    
}
