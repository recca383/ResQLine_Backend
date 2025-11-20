using Application.Abstractions.Messaging;
using Application.Reports.Create;
using Domain.Reports;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Todos;

internal sealed class Create : IEndpoint
{
    public sealed class Request
    {
        public Guid UserId { get; set; }
        public byte[] Image { get; set; }
        public Category Category { get; set; } = Category.None;
        public string Title { get; set; }
        public string? Description { get; set; }
        public Location ReportedAt { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("reports", async (
            Request request,
            ICommandHandler<CreateReportCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateReportCommand
            {
                UserId = request.UserId,
                Image = request.Image,
                Category = request.Category,
                Title = request.Title,
                Description = request.Description,
                ReportedAt = request.ReportedAt
            };

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Reports)
        .RequireAuthorization();
    }
}
