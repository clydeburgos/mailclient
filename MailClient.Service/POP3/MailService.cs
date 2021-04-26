using MailClient.Model;
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
            throw new NotImplementedException();
        }

        public Task DownloadBody()
        {
            throw new NotImplementedException();
        }

        public Task DownloadHeader()
        {
            throw new NotImplementedException();
        }
    }
}
