using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckChanges.Models.Holders
{
    class NewCasesData
    {
        public string Data { get; set; }
        public List<NewCasesHolder> NewCasesList { get; set; }

        public NewCasesData()
        {
            Data = string.Empty;
            NewCasesList = null;
        }

        public NewCasesData(string data, List<NewCasesHolder> newCasesHolders = null)
        {
            Data = data;
            if (newCasesHolders != null)
            {
                NewCasesList = newCasesHolders;
            }
        }
    }
}
