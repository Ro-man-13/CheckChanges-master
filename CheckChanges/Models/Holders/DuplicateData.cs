using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckChanges.Models.Holders
{
    class DuplicateData
    {
        public string Data { get; set; }
        public List<DuplicateHolder> DuplicateHolder { get; set; }

        public DuplicateData()
        {
            Data = string.Empty;
            DuplicateHolder = null;
        }

        public DuplicateData(string data, List<DuplicateHolder> duplicateHolder = null)
        {
            Data = data;
            if (duplicateHolder != null)
            {
                DuplicateHolder = duplicateHolder;
            }
        }
    }
}
