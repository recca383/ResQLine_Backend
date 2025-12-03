using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Application.Abstractions.Messaging;

namespace Application.Otps.Verify;
public sealed record VerifyOtpCommand(string mobileNumber, string otp) : ICommand<string>;
