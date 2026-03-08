using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Hubs;
using Domain.Reports.Events.Admin;
using Microsoft.AspNetCore.SignalR;
using SharedKernel;

namespace Application.Reports.Admin.PatchStatus;
internal sealed class PatchStatusCommandDomainEvent
    (
        IHubContext<NotificationHub> hubContext
    ): IDomainEventHandler<AdminReportPatchStatusDomainEvent>
{
    public Task Handle(AdminReportPatchStatusDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        hubContext.Clients.All.SendAsync("ReportStatusChanged", domainEvent.report, cancellationToken);

        return Task.CompletedTask;
    }
}
