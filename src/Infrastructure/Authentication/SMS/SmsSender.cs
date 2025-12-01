using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Authentication.SMS;

namespace Infrastructure.Authentication.SMS;
internal class SmsSender : ISmsSender
{
    public bool SendMessage(string MobileNumber, string Message)
    {
        Console.WriteLine("Sent to : " + MobileNumber + "\n" + Message);

        return true;
    }
}
