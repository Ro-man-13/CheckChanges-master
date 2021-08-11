using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckChanges.Models.Holders
{
    class NotMatchingRecordsHolder 
    {
        public string OurMailCase { get; set; }
        public string OurSheduleCase { get; set; }       
        public int Flag { get; set; }
        public int RegNo { get; set; }

        public NotMatchingRecordsHolder()
        {
            OurMailCase = string.Empty;
            OurSheduleCase = string.Empty;
            Flag = 0;
            RegNo = 0;
        }

        public NotMatchingRecordsHolder(string ourCase, string ourSheduleCase, int flag, int regNo)
        {
            OurMailCase = ourCase;
            OurSheduleCase = ourSheduleCase;
            Flag = flag;
            RegNo = regNo;
        }
    }
}
