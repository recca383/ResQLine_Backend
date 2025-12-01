using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Authentication.SMS;
public interface ISmsSender
{
    bool SendMessage(string MobileNumber, string Message);
}
