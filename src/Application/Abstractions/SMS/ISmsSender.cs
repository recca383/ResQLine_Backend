using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Authentication.SMS;
public interface ISmsSender
{
    Task<bool> SendMessage(string MobileNumber, string Message);
}
