using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Resources;
using System.Runtime.Versioning;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AI;
using Application.Abstractions.Authentication.SMS;
using Application.Abstractions.Data;
using Application.Abstractions.Hubs;
using Domain.Reports;
using Domain.Reports.Events;
using Domain.Users;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SharedKernel;
namespace Application.Reports.Create;


internal sealed class ReportCreatedCommandDomainEventHandler
    (
        IApplicationDbContext context,
        ISmsSender sender,
        ILogger logger,
        IDateTimeProvider dateTimeProvider,
        IHubContext<NotificationHub> hubContext
    ) :
    IDomainEventHandler<ReportCreatedDomainEvent>

{
    private sealed record ReportCreatedNotification(
        Guid Id,
        string Title,
        string Type,
        float Longitude,
        float Latitude,
        float Confidence,
        string Status,
        string TimeStamp,
        bool IsActive
    );

    public Task Handle(ReportCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        // == notify Users that the report was sent to responders ==
        User reportedby = context.Users
            .AsNoTracking()
            .FirstOrDefault(u => u.Id == domainEvent.report.ReportedById)!;

        Report report = context.Reports
            .FirstOrDefault(r => r.Id ==  domainEvent.report.Id);

        string reportCategory = report!.GetCategoryString();

        string date = dateTimeProvider.GetPhilippineTime(report.DateCreated);

        hubContext.Clients.All.SendAsync(
            "ReportCreated",
            new ReportCreatedNotification
            (
                report.Id,
                report.Description ?? "No Title",
                reportCategory,
                report.ReportedAt.Longitude,
                report.ReportedAt.Latitude,
                report.ReportedAt.Accuracy,
                Enum.GetName(report.Status)!,
                date,
                report.IsActive

            ),
            cancellationToken
            );


        sender.SendMessage(
            reportedby.MobileNumber,
            MessageTemplates.THANK_FOR_REPORTING
        );

        logger.Information(
            "Notified reporter {ReporterName} at {MobileNumber} about report submission.",
            reportedby.UserName ?? reportedby.FirstName + ' ' + reportedby.LastName,
            reportedby.MobileNumber
        );

        // == notify Responders about new report ==
        //List<string> PhoneNumbersofResponders = GetRespondersPhoneNumber(domainEvent.report.Category);

        //foreach (string number in PhoneNumbersofResponders)
        //{
        //    sender.SendMessage(
        //        number,
        //        MessageTemplates.CreateSummaryReport(domainEvent.report, dateTimeProvider, listofTags
        //    ));

        //    logger.Information(
        //        "Notified responder at {MobileNumber} about new report of category {Category}.",
        //        number,
        //        domainEvent.report.Category.ToString()
        //    );
        //}

        context.SaveChangesAsync(cancellationToken);
        return Task.CompletedTask;
    }

    

    //private List<string> GetRespondersPhoneNumber(Category category)
    //{
    //    List<string> phone_numbers = new();

    //    if (Department.Fire.And(category) == Department.Fire)
    //    {
    //        phone_numbers.Add("639150177937");
    //    }

    //    if (Department.Hospital.And(category) == Department.Hospital)
    //    {
    //        phone_numbers.Add("639814583389");
    //    }

    //    if (Department.Police.And(category) == Department.Police)
    //    {
    //        phone_numbers.Add("639310644503");
    //    }

    //    if (Department.Disaster_Response.And(category) == Department.Disaster_Response)
    //    {
    //        phone_numbers.Add("639092465965");
    //    }

    //    return phone_numbers;
    //}

    
}
