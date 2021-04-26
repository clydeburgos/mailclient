using MailClient.Core.Interface;
using MailClient.Model.Enum;
using MailClient.Service.Interface;
using System;

namespace MailClient.Core
{
    public class MailClientFactory : IMailClientFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public MailClientFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IMailService GetMailService(MailServerType paymentFactoryType)
        {
            switch (paymentFactoryType) {
                case MailServerType.IMAP:
                    return (IMailService)_serviceProvider.GetService(typeof(Service.IMAP.MailService));
                case MailServerType.POP3:
                    return (IMailService)_serviceProvider.GetService(typeof(Service.POP3.MailService));
                default:
                    return (IMailService)_serviceProvider.GetService(typeof(Service.IMAP.MailService));
            }
        }
    }
}
