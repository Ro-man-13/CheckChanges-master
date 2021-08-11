using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckChanges.Models.Holders
{
    class CheckMatchesHolder
    {
        public string OurCase { get; set; }
        public string Action { get; set; }
        public string Source { get; set; }
        public int Flag { get; set; }

        public CheckMatchesHolder()
        {
            OurCase = string.Empty;
            Action = string.Empty;
            Source = string.Empty;
            Flag = 0;
        }

        public CheckMatchesHolder(string ourCase, string action, string source, int flag)
        {
            OurCase = ourCase;
            Action = action;
            Source = source;
            Flag = flag;
        }
    }
}
