using Application.Abstractions.Messaging;

namespace Application.Reports.Get;

public sealed record GetReportQuery(
    string sort,
    int pageSize,
    int pageoffset
    
    
    ) : IQuery<List<ReportResponse>>;


