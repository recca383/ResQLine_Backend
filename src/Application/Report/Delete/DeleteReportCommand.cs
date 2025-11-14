using Application.Abstractions.Messaging;

namespace Application.Todos.Delete;

public sealed record DeleteReportCommand(Guid TodoItemId) : ICommand;
