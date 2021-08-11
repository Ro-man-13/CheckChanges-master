using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckChanges.Models.Holders
{
    class NewMailData
    {
        public string Data { get; set; }
        public List<NewMailHolder> NewMailHolder { get; set; }

        public NewMailData()
        {
            Data = string.Empty;
            NewMailHolder = null;
        }

        public NewMailData(string data, List<NewMailHolder> newMailHolder = null)
        {
            Data = data;
            if (newMailHolder != null)
            {
                NewMailHolder = newMailHolder;
            }
        }
    }
}
