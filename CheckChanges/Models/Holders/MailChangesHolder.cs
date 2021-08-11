using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckChanges.Models.Holders
{
    class MailChangesHolder
    {
        public string OurCase { get; set; }
        public string ChangedBy { get; set; }
        public int Flag { get; set; }

        public MailChangesHolder()
        {
            OurCase = string.Empty;
            ChangedBy = string.Empty;
            Flag = 0;
        }

        public MailChangesHolder(string ourCase, string alter, int flag)
        {
            OurCase = ourCase;
            ChangedBy = alter;
            Flag = flag;
        }
    }
}
