using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckChanges.Models.Holders
{
    class FileStorageHolder
    {
        public string OurCase { get; set; }
        public int Flag { get; set; }
        public string Link { get; set; }
        public string Content { get; set; }

        public FileStorageHolder()
        {
            OurCase = string.Empty;
            Flag = 0;
            Link = string.Empty;
            Content = string.Empty;
        }

        public FileStorageHolder(string ourCase, int flag, string link, string content)
        {
            OurCase = ourCase;
            Flag = flag;
            Link = link;
            Content = content;
        }
    }
}
