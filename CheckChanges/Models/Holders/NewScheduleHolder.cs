using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckChanges.Models.Holders
{
    class NewScheduleHolder
    {
        public string OurCase { get; set; }
        public string Creator { get; set; }
        public int Flag { get; set; }
        public string Action { get; set; }
        public string Status { get; set; }
        public bool Show { get; set; }

        public NewScheduleHolder()
        {
            OurCase = string.Empty;
            Creator = string.Empty;
            Flag = 0;
            Action = string.Empty;
            Status = string.Empty;
            Show = false;
        }

        public NewScheduleHolder(string ourCase, string creator, int flag, string action, string status, bool show)
        {
            OurCase = ourCase;
            Creator = creator;
            Flag = flag;
            Action = action;
            Status = status;
            Show = show;
        }
    }
}
