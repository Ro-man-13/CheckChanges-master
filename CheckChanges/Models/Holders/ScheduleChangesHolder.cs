using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckChanges.Models.Holders
{
    class ScheduleChangesHolder
    {
        public string OurCase { get; set; }
        public string ChangedBy { get; set; }
        public int Flag { get; set; }

        public ScheduleChangesHolder()
        {
            OurCase = string.Empty;
            ChangedBy = string.Empty;
            Flag = 0;
        }

        public ScheduleChangesHolder(string ourCase, string alter, int flag)
        {
            OurCase = ourCase;
            ChangedBy = alter;
            Flag = flag;
        }
    }
}
