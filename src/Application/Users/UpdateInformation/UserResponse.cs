using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.UpdateInformation;
public sealed record UserResponse
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
}
