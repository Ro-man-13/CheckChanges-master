using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckChanges.Models.Holders
{
    class CheckMatchesData
    {
        public string Data { get; set; }
        public List<CheckMatchesHolder> CheckMatchesHolder { get; set; }

        public CheckMatchesData()
        {
            Data = string.Empty;
            CheckMatchesHolder = null;
        }

        public CheckMatchesData(string data, List<CheckMatchesHolder> checkMatchesHolder = null)
        {
            Data = data;
            if (checkMatchesHolder != null)
            {
                CheckMatchesHolder = checkMatchesHolder;
            }
        }
    }
}
