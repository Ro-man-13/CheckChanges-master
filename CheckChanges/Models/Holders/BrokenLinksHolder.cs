using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckChanges.Models.Holders
{
    public class BrokenLinksHolder
    {
        public string OurCase { get; set; }
        public string Action { get; set; }
        public string Link { get; set; }
        public int Flag { get; set; }
        public string Content { get; set; }
        public string File { get; set; }
        public bool? InOut { get; set; }
        public bool? IgnoreDescription { get; set; }

        public BrokenLinksHolder()
        {
            OurCase = string.Empty;
            Action = string.Empty;
            Link = string.Empty;
            Flag = 0;
        }

        public BrokenLinksHolder(string ourCase, string action, string link, int flag)
        {
            OurCase = ourCase;
            Action = action;
            Link = link;
            Flag = flag;
        }
    }
}
