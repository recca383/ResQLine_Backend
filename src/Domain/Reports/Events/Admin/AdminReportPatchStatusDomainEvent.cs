using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel;

namespace Domain.Reports.Events.Admin;
public sealed record AdminReportPatchStatusDomainEvent(Report report) : IDomainEvent;
