using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Reports.Events;
using SharedKernel;

namespace Application.Reports.Create;
internal sealed class ReportCreatedCommandDomainEventHandler :
    IDomainEventHandler<ReportCreatedDomainEvent>
{
    public Task Handle(ReportCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {


        return Task.CompletedTask;
    }
}
