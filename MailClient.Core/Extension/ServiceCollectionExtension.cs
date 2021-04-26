using MailClient.Core;
using MailClient.Model;
using MailClient.Model.Config;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddMailService(this IServiceCollection services)
        {
            services.AddScoped<MailClientFactory>();
            services.AddScoped<MailRequestModel>();

            services.AddScoped<MailClient.Service.IMAP.MailService>()
            .AddScoped<MailClient.Service.Interface.IMailService, MailClient.Service.IMAP.MailService>
            (s => s.GetService<MailClient.Service.IMAP.MailService>());

            services.AddScoped<MailClient.Service.POP3.MailService>()
             .AddScoped<MailClient.Service.Interface.IMailService, MailClient.Service.POP3.MailService>
             (s => s.GetService<MailClient.Service.POP3.MailService>());

            return services;
        }
    }
}
