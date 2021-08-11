using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckChanges.Models.Holders
{
    class LostFilesHolder
    {
        public string OurCase { get; set; }
        public string Link { get; set; }
        public string File { get; set; }
        public int Flag { get; set; }

        public LostFilesHolder()
        {
            OurCase = string.Empty;
            Link = string.Empty;
            File = string.Empty;
            Flag = 0;
        }

        public LostFilesHolder(string ourCase, string link, string file, int flag)
        {
            OurCase = ourCase;
            Link = link;
            File = file;
            Flag = flag;
        }
    }
}
