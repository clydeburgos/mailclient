using Limilabs.Client.POP3;
using Limilabs.Mail;
using MailClient.Model;
using MailClient.Model.Enum;
using MailClient.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Service.POP3
{
    public class MailService : IMailService
    {
        private MailRequestModel _mailRequestModel;
        public MailService()
        {

        }

        public void SetProperties(MailRequestModel model)
        {
            _mailRequestModel = model;
        }

        public Task<bool> Connect()
        {
            try
            {
                using (Pop3 pop3 = new Pop3())
                {
                    bool isConnected = false;
                    EncryptionProcess(_mailRequestModel.EncryptionType, pop3);
                    pop3.Login(_mailRequestModel.Username, _mailRequestModel.Password);
                    isConnected = pop3.Connected;
                    pop3.Close();
                    return Task.FromResult(isConnected);
                }
            }
            catch (Exception ex)
            {
                //logger;
                throw ex;
            }
        }

        public Task DownloadBody()
        {
            throw new NotImplementedException();
        }

        public Task DownloadHeader()
        {
            throw new NotImplementedException();
        }

        private Pop3 EncryptionProcess(EncryptionType encType, Pop3 pop3)
        {
            switch (encType)
            {
                case EncryptionType.SSLTLS:
                    pop3.ConnectSSL(_mailRequestModel.Server, _mailRequestModel.Port);
                    return pop3;
                case EncryptionType.STARTTLS:
                    pop3.Connect(_mailRequestModel.Server, _mailRequestModel.Port);
                    pop3.StartTLS();
                    return pop3;
                default:
                    pop3.ConnectSSL(_mailRequestModel.Server, _mailRequestModel.Port);
                    return pop3;
            }
        }
    }
}
