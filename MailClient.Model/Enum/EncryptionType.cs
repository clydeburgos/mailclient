using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MailClient.Model.Enum
{
    public enum EncryptionType
    {
        [Display(Name = "Unencrypted")]
        Unencrypted,
        [Display(Name = "SSL/TLS")]
        SSLTLS,
        [Display(Name = "STARTTLS")]
        STARTTLS
    }
}
