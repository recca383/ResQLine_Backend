namespace Web.Api.Endpoints.Requests;

public sealed record GetRequest(
    string sort,
    int pageSize,
    int pageoffset);
