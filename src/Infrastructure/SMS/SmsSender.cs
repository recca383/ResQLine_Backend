using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Authentication.SMS;
using Microsoft.Extensions.Configuration;


namespace Infrastructure.Authentication.SMS;
internal class SmsSender(
    IConfiguration configuration,
    HttpClient client
    ) : ISmsSender
{
    public async Task<bool> SendMessage(string MobileNumber, string Message)
    {
        string ApiKey = configuration.GetValue<string>("Semaphore:APIKey");
        string SemaphoreSendMessageLink = configuration.GetValue<string>("Semaphore:MessageLink");
        
        try
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(SemaphoreSendMessageLink, new
            {
                apikey = ApiKey,
                number = MobileNumber,
                message = Message,
                sendername = "ResQLine"

            });

            return response.IsSuccessStatusCode;
        }
        catch( Exception ex )
        {
            throw new ApplicationException( ex.Message );
        }

    }
}
