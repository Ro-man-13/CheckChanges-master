using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckChanges.Models.Holders
{
    class NewCasesHolder 
    {
        public string OurCase { get; set; }
        public string Creator { get; set; }
        public DateTime? User_Cr_Date { get; set; }
        public int Flag { get; set; }

        public NewCasesHolder()
        {
            OurCase = string.Empty;
            Creator = string.Empty;
            Flag = 0;
        }

        public NewCasesHolder(string ourCase, string creator, int flag)
        {
            OurCase = ourCase;
            Creator = creator;
            Flag = flag;
        }
    }
}
