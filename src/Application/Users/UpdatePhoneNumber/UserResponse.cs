using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.UpdatePhoneNumber;
public sealed record UserResponse
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string MobileNumber { get; init; }
}
