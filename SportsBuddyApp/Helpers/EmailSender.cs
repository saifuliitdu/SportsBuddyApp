using Microsoft.Extensions.Configuration;
using RestSharp;
using RestSharp.Authenticators;
using SportsBuddy.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CutOutWizWebApp.Helpers
{
    public class EmailSender
    {
        public string ReceverEmail { get; set; }
        public string BaseUrl { get; set; }
        public string Domain { get; set; }
        public string ApiKey { get; set; }
        public string EmailFrom { get; set; }
        private readonly IConfiguration _configuration;
        public EmailSender(IConfiguration configuration)
        {
            // ReceverEmail = receverEmail;
            _configuration = configuration;
            BaseUrl = _configuration.GetSection("AppSettings").GetSection("mailgun.baseurl").Value;
            Domain = _configuration.GetSection("AppSettings").GetSection("mailgun.domain").Value;
            ApiKey = _configuration.GetSection("AppSettings").GetSection("mailgun.apikey").Value;
            EmailFrom = _configuration.GetSection("AppSettings").GetSection("mailgun.emailsender").Value;
        }

        public bool SendEmai(string subject, string emailBody)
        {
            try
            {
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential(_configuration.GetSection("AppSettings").GetSection("smtp.email").Value, _configuration.GetSection("AppSettings").GetSection("smtp.emailPass").Value);
                smtp.EnableSsl = true;

                MailAddress from = new MailAddress(_configuration.GetSection("AppSettings").GetSection("smtp.email").Value, "CutOutWiz",
               System.Text.Encoding.UTF8);
                // Set destinations for the e-mail message.
                MailAddress to = new MailAddress(ReceverEmail);
                // Specify the message content.
                MailMessage message = new MailMessage(from, to);
                message.Body = emailBody;

                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.Subject = subject;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                smtp.Send(message);
                return true;
            }
            catch (Exception e)
            {
                Helper.Log(e.ToString());
                return false;
            }
        }
        public bool SendMailGun(string subject, string emailBody, List<string> recievers,string type)
        {
            try
            {
                RestClient client = new RestClient();
                client.BaseUrl = new Uri("https://api.mailgun.net/v3");
                client.Authenticator = new HttpBasicAuthenticator("api", ApiKey);
                RestRequest request = new RestRequest();
                request.AddParameter("domain", Domain, ParameterType.UrlSegment);
                request.Resource = "{domain}/messages";
                request.AddParameter("from", EmailFrom);
                foreach (var address in recievers)
                {
                    request.AddParameter("to", address.Trim());
                }
                request.AddParameter("subject", subject);
                request.AddParameter(type, emailBody);
                request.Method = Method.POST;
                var x = client.Execute(request);
            }
            catch (Exception e)
            {
                Helper.Log(e.ToString());
                return false;
            }
            return true;
        }
    }
}
