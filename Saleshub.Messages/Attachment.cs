using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saleshub.Messages
{
    public class Attachment
    {
        public string Name { get; set; }

        public string Content { get; set; }

        public string ContentType { get; set; }

        public string ContentID { get; set; }

        public long ContentLength { get; set; }
    }
}
