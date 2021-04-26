using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MailClient.Model.Enum
{
    public enum MailServerType
    {
        [Display(Name = "IMAP")]
        IMAP,
        [Display(Name = "POP3")]
        POP3
    }
}
