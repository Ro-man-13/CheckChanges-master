using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckChanges.Models.Holders
{
    class BrokenLinksData
    {
        public string Data { get; set; }
        public List<BrokenLinksHolder> BrokenLinksHolder { get; set; }

        public BrokenLinksData()
        {
            Data = string.Empty;
            BrokenLinksHolder = null;
        }

        public BrokenLinksData(string data, List<BrokenLinksHolder> brokenLinksHolder = null)
        {
            Data = data;
            if (brokenLinksHolder != null)
            {
                BrokenLinksHolder = brokenLinksHolder;
            }
        }
    }
}
