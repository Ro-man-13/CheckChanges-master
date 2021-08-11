using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckChanges.Models.Holders
{
    class MailChangesData
    {
        public string Data { get; set; }
        public List<MailChangesHolder> MailChangesHolder { get; set; }

        public MailChangesData()
        {
            Data = string.Empty;
            MailChangesHolder = null;
        }

        public MailChangesData(string data, List<MailChangesHolder> mailChangesHolder = null)
        {
            Data = data;
            if (mailChangesHolder != null)
            {
                MailChangesHolder = mailChangesHolder;
            }
        }
    }
}
