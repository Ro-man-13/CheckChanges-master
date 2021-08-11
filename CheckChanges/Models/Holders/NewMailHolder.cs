using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckChanges.Models.Holders
{
    class NewMailHolder
    {
        public string OurCase { get; set; }
        public string Creator { get; set; }
        public int Flag { get; set; }
        public string Content { get; set; }

        public NewMailHolder()
        {
            OurCase = string.Empty;
            Creator = string.Empty;
            Flag = 0;
            Content = string.Empty;
        }

        public NewMailHolder(string ourCase, string creator, int flag, string content)
        {
            OurCase = ourCase;
            Creator = creator;
            Flag = flag;
            Content = content;
        }
    }
}
