using SharedKernel;

namespace Domain.Users;

public static class UserErrors
{
    public static Error NotFound(Guid userId) => Error.NotFound(
        "Users.NotFound",
        $"The user with the Id = '{userId}' was not found");

    public static Error NotVerified => Error.Problem(
        "Users.NotVerified",
        $"The user has cancelled verification");

    public static Error Unauthorized() => Error.Failure(
        "Users.Unauthorized",
        "You are not authorized to perform this action.");

    public static readonly Error NotFoundByMobileNumber = Error.NotFound(
        "Users.MobileNumberNotFound",
        "The user with the specified mobile number was not found");

    public static readonly Error MobileNumberNotUnique = Error.Conflict(
        "Users.MobileNumberAlreadyTaken",
        "The provided mobile number is not unique");

    public static Error AlreadySet(string mobileNumber) => Error.Conflict(
        "Users.AlreadySet",
        $"The mobile number: {mobileNumber} is already set");
}
