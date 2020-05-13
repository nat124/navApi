using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace TestCore
{
     public static class Example
    {
        //private static void Main()
        //{
        //    Execute().Wait();
        //}
        private static IOptions<AppSettings> _settings;

        public static async Task Execute(Emailmodel Emailmodel)
        {
            
          //  var apiKey = Environment.GetEnvironmentVariable(key);
            var client = new SendGridClient(Emailmodel.key);
            var from = new EmailAddress("support@pistis.com.mx");
            //var subject = "Forgot Password";
            var to = new EmailAddress(Emailmodel.To);
            var plainTextContent = "Greetings";
            //var htmlContent = "<strong> As you requested, your password for your account has now been reset. Your new password is  " + password + " . If it was not at your request, then please contact support immediately.;</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, Emailmodel.Subject, plainTextContent,  Emailmodel.Body);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
