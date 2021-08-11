using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckChanges.Models.Holders
{
    class CasesChangesHolder
    {
        public string OurCase { get; set; }
        public string Last_User { get; set; }
        public int Flag { get; set; }
        public DateTime? User_Cr_Date { get; set; }
        public DateTime? Last_User_Date { get; set; }

        public CasesChangesHolder()
        {
            OurCase = string.Empty;
            Last_User = string.Empty;
            Flag = 0;
        }

        public CasesChangesHolder(string ourCase, string alter, int flag)
        {
            OurCase = ourCase;
            Last_User = alter;
            Flag = flag;
        }
    }
}
