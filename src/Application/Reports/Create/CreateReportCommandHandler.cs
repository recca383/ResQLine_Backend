using System.Globalization;
using System.Text;
using AI;
using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Reports;
using Domain.Reports.Events;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedKernel;

namespace Application.Reports.Create;

internal sealed class CreateReportCommandHandler(
    IApplicationDbContext context,
    IDateTimeProvider dateTimeProvider,
    IUserContext userContext
   )
    : ICommandHandler<CreateReportCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateReportCommand command, CancellationToken cancellationToken)
    {
        User? user = await context.Users.AsNoTracking()
            .SingleOrDefaultAsync(u => u.Id == userContext.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound(userContext.UserId));
        }

        var report = new Report
        {
            Id = Guid.NewGuid(),
            ReportedById = user.Id,
            Image = command.Image,
            Description = command.Description,
            ReportedAt = command.ReportedAt,
            Status = Status.Submitted,
            Category = command.Category,
            IsDeleted = false,
            DateCreated = dateTimeProvider.UtcNow,
            Priority = Priority.Low
        };
        
        GetListOfTags(report);
        // Send an email to the department and the user who reported it
        report.Raise(new ReportCreatedDomainEvent(report));

        context.Reports.Add(report);

        await context.SaveChangesAsync(cancellationToken);

        return report.Id;
    }

    public string GetListOfTags(Report report)
    {
        var tags = new StringBuilder();

        var multimodalClassification = new ImageClassification();

        //logger.Information("Performing image classification for report ID {ReportId}.", report.Id);
        Dictionary<string, float> predictedTags = multimodalClassification.Predict(report.Image);


        //if (!predictedTags.Any())
        //{
        //    tags.AppendLine("No significant tags detected.");
        //}
        //else
        //{
        //    foreach (KeyValuePair<string, float> predictedpair in predictedTags)
        //    {
        //        tags.AppendLine(CultureInfo.InvariantCulture, $"- {predictedpair.Key} : {predictedpair.Value * 100}%");
        //    }
        //}
        report.AIProbabilities = predictedTags;

        //logger.Information("Image classification completed for report ID {ReportId}. Detected tags: {Tags}.", report.Id, string.Join(", ", predictedTags));

        multimodalClassification.Dispose();

        return tags.ToString();
    }
}
