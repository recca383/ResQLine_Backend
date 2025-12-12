using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Authentication.SMS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Authentication.SMS;
internal class SmsSender(
    IConfiguration configuration,
    HttpClient client,
    ILogger<SmsSender> logger
    ) : ISmsSender
{
    public async Task<bool> SendMessage(string MobileNumber, string Message)
    {
        string ApiKey = configuration.GetValue<string>("SmsMobile:ApiKey");
        string MessageLink = configuration.GetValue<string>("SmsMobile:MessageLink");

        try
        {
            logger.LogInformation("Creating http request on : {MobileNumber}", MobileNumber);
            HttpResponseMessage response = await client.PostAsJsonAsync(MessageLink, new
            {
                apikey = ApiKey,
                recipients = MobileNumber,
                message = Message
            });

            logger.LogInformation("Http request sent on : {MobileNumber}", MobileNumber);

            if(!response.IsSuccessStatusCode)
            {
                logger.LogError("Http request failed on : {MobileNumber}", response.Content);
            }

            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException ex)
        {
            throw new HttpRequestException(ex.Message);
        }
        catch ( Exception ex )
        {
            throw new ApplicationException( ex.Message );
        }

    }

    public async Task<bool> SendMessagev2(string MobileNumber, string Message)
    {
        string ApiKey = configuration.GetValue<string>("Semaphore:APIKey");
        string MessageLink = configuration.GetValue<string>("Semaphore:MessageLink");
        string senderName = configuration.GetValue<string>("Semaphore:SenderName");

        try
        {

            HttpResponseMessage response = await client.PostAsJsonAsync(MessageLink, new
            {
                apikey = ApiKey,
                number = MobileNumber,
                message = Message,
                sendername = senderName
            });

            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException ex)
        {
            throw new HttpRequestException(ex.Message);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }
}
