using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AI;
using Application.Abstractions.Authentication.SMS;
using Application.Abstractions.Data;
using Domain.Reports;
using Domain.Reports.Events;
using Domain.Users;
using Serilog;
using SharedKernel;

namespace Application.Reports.Create;
internal sealed class ReportCreatedCommandDomainEventHandler
    (
        IApplicationDbContext context,
        ISmsSender sender,
        ILogger logger
    ):
    IDomainEventHandler<ReportCreatedDomainEvent>

{
    private const string MESSAGE_FOR_RESPONDERS =
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

    public Task Handle(ReportCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        // == notify Users that the report was sent to responders ==
        User reportedby = context.Users
            .FirstOrDefault(u => u.Id == domainEvent.report.ReportedBy)!;

        sender.SendMessage(
            reportedby.MobileNumber,
            "Thank you for your report. Emergency responders have been notified and are on their way to assist you."
        );

        logger.Information(
            "Notified reporter {ReporterName} at {MobileNumber} about report submission.",
            reportedby.UserName ?? reportedby.FirstName + ' ' + reportedby.LastName,
            reportedby.MobileNumber
        );

        // == notify Responders about new report ==
        List<string> PhoneNumbersofResponders = GetRespondersPhoneNumber(domainEvent.report.Category);

        foreach(string number in PhoneNumbersofResponders)
        {
            sender.SendMessage(
                number,
                GetMessage(domainEvent.report, domainEvent.report.Category)
            );
            logger.Information(
                "Notified responder at {MobileNumber} about new report of category {Category}.",
                number,
                domainEvent.report.Category.ToString()
            );
        }
        return Task.CompletedTask;
    }
    private string GetMessage(Report report, Category category)
    {
        string message = MESSAGE_FOR_RESPONDERS
                         .Replace("{incident}", GetCategory(category))
                         .Replace("{description}", string.IsNullOrWhiteSpace(report.Description) ?
                                                   "No additional notes provided."
                                                   : report.Description)
                         .Replace("{ListOfTags}", GetListOfTags(report))
                         .Replace("{latitude}", report.ReportedAt.Latitude.ToString(CultureInfo.InvariantCulture))
                         .Replace("{longitude}", report.ReportedAt.Longitude.ToString(CultureInfo.InvariantCulture))
                         .Replace("{Timestamp}", GetPhilippineTime(report.DateCreated))
                         .Replace("{location}", report.ReportedAt.ReverseGeoCode);

        return message;
    }

    private string GetListOfTags(Report report)
    {
        var tags = new StringBuilder();

        var imageClassification = new ImageClassification();

        logger.Information("Performing image classification for report ID {ReportId}.", report.Id);
        HashSet<string> predictedTags = imageClassification.Predict(report.Image);

        if (predictedTags.Count == 0)
        {
            tags.AppendLine("No significant tags detected.");
        }
        else
        {
            foreach (string tag in predictedTags)
            {
                tags.AppendLine(CultureInfo.InvariantCulture,$"- {tag}");
            }
        }

        logger.Information("Image classification completed for report ID {ReportId}. Detected tags: {Tags}.", report.Id, string.Join(", ", predictedTags));

        imageClassification.Dispose();

        return tags.ToString();
    }

    private string GetCategory(Category category) => category switch
    {
        Category.Fire_Incident => "Fire",
        Category.Flooding => "Flooding",
        Category.Medical_Emergency => "Medical Emergency",
        Category.Structural_Damage => "Structural Damage",
        Category.Traffic_Accident => "Traffic Accident",
        Category.Other_General_Incident => "",
        _ => "Unknown"
    };

    private string GetPhilippineTime(DateTime dateTimeToConvert)
    {
        string philippinesTimeZoneId = "Singapore Standard Time";

        var philippinesZone = TimeZoneInfo.FindSystemTimeZoneById(philippinesTimeZoneId);

        DateTime philippineTime = TimeZoneInfo.ConvertTime(dateTimeToConvert, philippinesZone);

        return philippineTime.ToString("MMM dd, yyyy hh:mm", CultureInfo.InvariantCulture);
    }
    private List<string> GetRespondersPhoneNumber(Category category)
    {
        List<string> phone_numbers = new();

        if(And(Department.Fire, category) == Department.Fire)
        {
            phone_numbers.Add("639150177937");
        }

        if (And(Department.Hospital, category) == Department.Hospital)
        {
            phone_numbers.Add("639814583389");
        }

        if (And(Department.Police, category) == Department.Police)
        {
            phone_numbers.Add("639310644503");
        }

        if (And(Department.Disaster_Response, category) == Department.Disaster_Response)
        {
            phone_numbers.Add("639092465965");
        }

        return phone_numbers;
    }
    private static TFlags And<TFlags, TEnum>(TFlags flags, TEnum value)
    where TFlags : Enum
    where TEnum : Enum
    {
        int result = Convert.ToInt32(flags, CultureInfo.InvariantCulture) 
                    & Convert.ToInt32(value, CultureInfo.InvariantCulture);

        return (TFlags)Enum.ToObject(typeof(TFlags), result);
    }

}
