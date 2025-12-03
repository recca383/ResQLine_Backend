using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;
using Domain.Users;

namespace Application.Users.UpdatePhoneNumber;
public sealed record UpdatePhoneNumberCommand(
    string MobileNumber)
    : ICommand<UserResponse>;
