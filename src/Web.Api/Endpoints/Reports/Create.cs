using Application.Abstractions.Messaging;
using Application.Reports.Create;
using Domain.Reports;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Reports;

internal sealed class Create : IEndpoint
{
    public sealed class Request
    {
        public Guid UserId { get; set; }
        public string Image { get; set; }
        public Category Category { get; set; } = Category.None;
        public string Title { get; set; }
        public string? Description { get; set; }
        public Location Location { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("reports", async (
            Request request,
            ICommandHandler<CreateReportCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            byte[] image = Convert.FromBase64String(request.Image);

            var command = new CreateReportCommand
            {
                UserId = request.UserId,
                Image = image,
                Category = request.Category,
                Title = request.Title,
                Description = request.Description,
                ReportedAt = request.Location
            };

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Reports)
        .RequireAuthorization();
    }
}
