using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;

namespace Application.Otps.Register.Send;
public sealed record SendRegisterOtpCommand(string MobileNumber) : ICommand;
