using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckChanges.Models.Holders
{
    class NotMatchingRecordsData
    {
        public string Data { get; set; }
        public List<NotMatchingRecordsHolder> NotMatchingRecordsHolder { get; set; }

        public NotMatchingRecordsData()
        {
            Data = string.Empty;
            NotMatchingRecordsHolder = null;
        }

        public NotMatchingRecordsData(string data, List<NotMatchingRecordsHolder> notMatchingRecordsHolder = null)
        {
            Data = data;
            if (notMatchingRecordsHolder != null)
            {
                NotMatchingRecordsHolder = notMatchingRecordsHolder;
            }
        }
    }
}
