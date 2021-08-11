using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckChanges.Models.Holders
{
    class LostFilesData
    {
        public string Data { get; set; }
        public List<LostFilesHolder> LostFilesHolder { get; set; }

        public LostFilesData()
        {
            Data = string.Empty;
            LostFilesHolder = null;
        }

        public LostFilesData(string data, List<LostFilesHolder> lostFilesHolder = null)
        {
            Data = data;
            if (lostFilesHolder != null)
            {
                LostFilesHolder = lostFilesHolder;
            }
        }
    }
}
