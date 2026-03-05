using Application.Abstractions.Messaging;

namespace Application.Reports.Admin.Get;

public sealed record GetReportQuery(
    string sort,
    int pageSize,
    int pageoffset
    
    
    ) : IQuery<List<ReportResponse>>;


