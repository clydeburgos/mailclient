using MailClient.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Service.Interface
{
    public interface IMailService
    {
        void SetProperties(MailRequestModel model);
        Task<bool> Connect();
        Task<List<MailResponseModel>> DownloadHeader();
        Task DownloadBody();
    }
}
