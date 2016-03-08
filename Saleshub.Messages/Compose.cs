using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saleshub.Messages
{
    public class Compose
    {
        public string SourceId { get; set; }

        public string FromName { get; set; }

        public string From { get; set; }

        public Full FromFull { get; set; }

        public string To { get; set; }

        public IList<Full> ToFull { get; set; }

        public string Cc { get; set; }

        public IList<Full> CcFull { get; set; }

        public string Bcc { get; set; }

        public IList<Full> BccFull { get; set; }

        public string OriginalRecipient { get; set; }

        public string Subject { get; set; }

        public string MessageID { get; set; }

        public string ReplyTo { get; set; }

        public string MailboxHash { get; set; }

        public string Date { get; set; }

        public string TextBody { get; set; }

        public string HtmlBody { get; set; }

        public string StrippedTextReply { get; set; }

        public string Tag { get; set; }

        public IList<Header> Headers { get; set; }

        public IList<Attachment> Attachments { get; set; }
    }
}
