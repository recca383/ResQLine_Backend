using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        logger.LogInformation("Creating http request on : {MobileNumber}", MobileNumber);
        var uriBuilder = new UriBuilder(MessageLink!);
        System.Collections.Specialized.NameValueCollection? query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);
        query["apikey"] = ApiKey;
        query["recipients"] = MobileNumber;
        query["message"] = Message;

        uriBuilder.Query = query.ToString();

        string url = uriBuilder.ToString();
        try
        {
            HttpResponseMessage response = await client.PostAsync(url, null);

            logger.LogInformation("Http request sent on : {MobileNumber}", MobileNumber);
            logger.LogInformation("Http request response on : {MobileNumber} => SENT!", MobileNumber);

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
