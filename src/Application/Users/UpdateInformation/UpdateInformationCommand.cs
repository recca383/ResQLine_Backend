using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Application.Abstractions.Messaging;

namespace Application.Users.UpdateInformation;
public sealed record UpdateInformationCommand(
    string FirstName,
    string LastName,
    string UserName)
    : ICommand<UserResponse>;
