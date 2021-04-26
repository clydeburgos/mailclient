using System;
using System.Collections.Generic;
using System.Text;

namespace MailClient.Model.Config
{
    public class EmailProviderConfig
    {
        public string SMTPServerUrl { get; set; } = "smtp.gmail.com";
        public string EmailAddress { get ;set; } = "clydedeveloperburgos@gmail.com";
        public string Password { get; set; } = "clerick100390";
    }
}
