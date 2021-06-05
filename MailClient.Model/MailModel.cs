using MailClient.Model.Enum;
using System;
using System.Collections.Generic;

namespace MailClient.Model
{
    public class MailRequestModel {

        public MailServerType ServerType { get; set; }
        public EncryptionType EncryptionType { get; set; }
        public int Port { get; set; }
        public string Server { get; set; } = "imap.gmail.com";
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }


    public class MailResponseModel
    {
        public string UID { get; set; }
        public List<string> To { get; set; }
        public DateTime Date { get; set; }
        public List<string> From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
