using Application.Abstractions.Messaging;

namespace Application.Todos.Complete;

public sealed record CompleteReportCommand
    (Guid TodoItemId) : ICommand;
