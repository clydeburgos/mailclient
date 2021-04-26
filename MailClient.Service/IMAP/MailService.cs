﻿using Limilabs.Client.IMAP;
using Limilabs.Mail;
using MailClient.Model;
using MailClient.Model.Config;
using MailClient.Model.Enum;
using MailClient.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Service.IMAP
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
            try {
                using (Imap imap = new Imap())
                {
                    EncryptionProcess(_mailRequestModel.EncryptionType, imap);

                    imap.UseBestLogin(_mailRequestModel.Username, _mailRequestModel.Password);
                    imap.SelectInbox();

                    List<long> uids = imap.Search(Flag.Unseen);

                    foreach (long uid in uids)
                    {
                        var eml = imap.GetMessageByUID(uid);
                        IMail email = new MailBuilder().CreateFromEml(eml);

                        Console.WriteLine(email.Subject);
                    }

                    imap.Close();
                    return Task.FromResult(true);
                }
            } catch (Exception ex) 
            {
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

        private Imap EncryptionProcess(EncryptionType encType, Imap imap) {
            switch (encType) {
                case EncryptionType.SSLTLS:
                    imap.ConnectSSL(_mailRequestModel.Server, _mailRequestModel.Port);
                    return imap;
                case EncryptionType.STARTTLS:
                    imap.Connect(_mailRequestModel.Server, _mailRequestModel.Port);
                    imap.StartTLS();
                    return imap;
                default:
                    imap.ConnectSSL(_mailRequestModel.Server, _mailRequestModel.Port);
                    return imap;
            }
        }

    }
}