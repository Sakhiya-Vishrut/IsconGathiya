using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsconGathiya.Common
{
    public class EmailParameter
    {
        public string? MailTo { get; set; }
        public string? Mailcc { get; set; }
        public string? MailBcc { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public string? Attachment { get; set; }
        public bool IsHtml { get; set; }

        public string? FileName { get; set; }
        public string? FailureReason { get; set; }
        public bool? IsAttachmentContent { get; set; }
    }
}
