using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckChanges.Models.Holders
{
    class CasesChangesData
    {
        public string Data { get; set; }
        public List<CasesChangesHolder> CasesChangesHolder { get; set; }

        public CasesChangesData()
        {
            Data = string.Empty;
            CasesChangesHolder = null;
        }

        public CasesChangesData(string data, List<CasesChangesHolder> casesChangesHolder = null)
        {
            Data = data;
            if (casesChangesHolder != null)
            {
                CasesChangesHolder = casesChangesHolder;
            }
        }
    }
}
