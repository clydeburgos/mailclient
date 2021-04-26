using MailClient.Model.Enum;
using MailClient.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailClient.Core.Interface
{
    public interface IMailClientFactory
    {
        IMailService GetMailService(MailServerType paymentFactoryType);
    }
}
