using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckChanges.Models.Holders
{
    class DuplicateHolder
    {
        public string OurCase { get; set; }
        public int Flag { get; set; }

        public DuplicateHolder()
        {
            OurCase = string.Empty;
            Flag = 0;
        }

        public DuplicateHolder(string ourCase, int flag)
        {
            OurCase = ourCase;
            Flag = flag;
        }
    }
}
