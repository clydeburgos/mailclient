using Limilabs.Client.IMAP;
using Limilabs.Mail;
using MailClient.Model;
using MailClient.Model.Config;
using MailClient.Model.Enum;
using MailClient.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace MailClient.Service.IMAP
{
    public class MailService : IMailService
    {
        private MailRequestModel _mailRequestModel;
        private Imap _map { get; set; }
        public List<MailResponseModel> _mailResponses { get; set; }
        public MailService()
        {
        }

        public void SetProperties(MailRequestModel model)
        {
            _mailRequestModel = model;
        }

        public Task<bool> Connect() //IsConnected
        {
            try
            {
                using (var map = new Imap())
                {
                    bool isConnected = false;
                    EncryptionProcess(_mailRequestModel.EncryptionType, map);
                    map.UseBestLogin(_mailRequestModel.Username, _mailRequestModel.Password);
                    isConnected = map.Connected;
                    if (isConnected) _map = map;
                    return Task.FromResult(isConnected);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Task<Imap> ConnectAndReturn() {
            try
            {
                using (var map = new Imap())
                {
                    EncryptionProcess(_mailRequestModel.EncryptionType, map);
                    map.UseBestLogin(_mailRequestModel.Username, _mailRequestModel.Password);

                    return Task.FromResult(map);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task DownloadBody()
        {
            throw new NotImplementedException();
        }

        public Task<List<MailResponseModel>> DownloadHeader()
        {
            try
            {
                
                using (var map = new Imap()) {

                    EncryptionProcess(_mailRequestModel.EncryptionType, map);
                    map.UseBestLogin(_mailRequestModel.Username, _mailRequestModel.Password);

                    map.SelectInbox();
                    List<long> uids = map.Search(Flag.New);
                    List<MessageInfo> infos = map.GetMessageInfoByUID(uids);

                    foreach (MessageInfo info in infos)
                    {
                        AddMessageInfoToMemory(info);

                        //foreach (MimeStructure attachment in info.BodyStructure.Attachments)
                        //{
                        //    Console.WriteLine("  Attachment: '{0}' ({1} bytes)",
                        //        attachment.SafeFileName,
                        //        attachment.Size);
                        //}
                        //Console.WriteLine();
                    }

                    return Task.FromResult(_mailResponses);
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Task AddMessageInfoToMemory(MessageInfo messageInfo)
        {
            var mailResponseModel = new MailResponseModel()
            {
                UID = messageInfo.UID.ToString(),
                From = messageInfo.Envelope.From.Select(f => f.Address).ToList(),
                To = messageInfo.Envelope.To.Select(t => t.Name).ToList(),
                Date = messageInfo.Envelope.Date.Value
            };

            _mailResponses.Add(mailResponseModel);
            AddMessageInfoMessage(mailResponseModel);

            return Task.CompletedTask;
        }

        private Task AddMessageInfoMessage(MailResponseModel responseModel)
        {
            //LOG
            //Console.WriteLine("Subject: " + info.Envelope.Subject);
            //Console.WriteLine("From: " + info.Envelope.From);
            //Console.WriteLine("To: " + info.Envelope.To);
            return Task.CompletedTask;
        }

        private Imap EncryptionProcess(EncryptionType encType, Imap imap)
        {
            switch (encType)
            {
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
