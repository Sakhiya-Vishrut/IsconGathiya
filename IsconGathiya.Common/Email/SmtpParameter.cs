using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsconGathiya.Common
{
    public class SmtpParameter
    {
        public string? MailFrom { get; set; }

        public string? SMTPHost { get; set; }

        public string? SMTPUserName { get; set; }

        public string? SMTPPassword { get; set; }

        public int SMTPPort { get; set; }

        public bool IsSMTPEnableSsl { get; set; }

    }
}
