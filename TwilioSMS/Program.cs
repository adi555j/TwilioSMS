// Install the C# / .NET helper library from twilio.com/docs/csharp/install

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;


class TwilioSMS
{

    private readonly ILogger _logger;



    //inject the logger 
    public TwilioSMS(ILogger<TwilioSMS> logger)
    {
        _logger = logger;
    }

    public TwilioSMS()
    {
    }

    public static void Main(string[] args)
    {
        //adding numbers to a list collection (ignore this , I know this is bad :( )
        List<SMS> sms = new List<SMS>();
        SMS s0 = new SMS();
        s0.PhoneTo = "1112223333";
        s0.PhoneFrom = "111";
        s0.SmsBody = "Test";
        sms.Add(s0);

        SMS s1 = new SMS();
        s1.PhoneTo = "2223334444";
        s1.PhoneFrom = "111";
        s1.SmsBody = "Test";
        sms.Add(s1);

        SMS s2 = new SMS();
        s2.PhoneTo = "3334445555";
        s2.PhoneFrom = "111";
        s2.SmsBody = "Test";
        sms.Add(s2);

        SMS s3 = new SMS();
        s3.PhoneTo = "1234567890";
        s3.PhoneFrom = "111";
        s3.SmsBody = "Test";
        sms.Add(s3);

        SMS s4 = new SMS();
        s4.PhoneTo = "9876543210";
        s4.PhoneFrom = "111";
        s4.SmsBody = "Test";
        sms.Add(s4);

        TwilioSMS t = new TwilioSMS();
        foreach (var s in sms)
        {
            // do not await and loop through the numbers
             t.SendSms(s).ConfigureAwait(false);
        }
    }

    //send sms asynchronously
    public async Task SendSms(SMS sms)
    {
        try
        {
            string accountSid = "TWILIO_ACCOUNT_SID";
            string authToken = "TWILIO_AUTH_TOKEN";

            TwilioClient.Init(accountSid, authToken);


            //send message asynchronously
            var message = await MessageResource.CreateAsync(
                body: sms.SmsBody,
                from: new Twilio.Types.PhoneNumber(sms.PhoneFrom),
                to: new Twilio.Types.PhoneNumber(sms.PhoneTo)
            );

            if(message.Sid != null)
            {
                _logger.LogInformation(
                    "Sending SMS with sid:" + message.Sid +
                    "\n" + "From:" + sms.PhoneFrom +
                    "\n" + "To:" + sms.PhoneTo +
                    "\n" + "Date:" + DateTime.UtcNow +
                    "\n" + "Body" + sms.SmsBody
                    );
            }

        }
        catch(Exception e)
        {
            _logger.LogError(
                "Sending SMS failed with error message:" + e.Message + 
                "\n" + "The stack trace is:" + e.StackTrace +
                "\n" + "From:" + sms.PhoneFrom + 
                "\n" + "To:" + sms.PhoneTo + 
                "\n" + "Date:" + DateTime.UtcNow +
                "\n" + "Body" + sms.SmsBody
                );
        }

    }
}


//sms model
public class SMS
{
    public string PhoneFrom;
    public string PhoneTo;
    public string SmsBody;
}