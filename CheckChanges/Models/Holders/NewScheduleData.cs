using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckChanges.Models.Holders
{
    class NewScheduleData
    {
        public string Data { get; set; }
        public List<NewScheduleHolder> NewScheduleHolder { get; set; }

        public NewScheduleData()
        {
            Data = string.Empty;
            NewScheduleHolder = null;
        }

        public NewScheduleData(string data, List<NewScheduleHolder> newScheduleHolder = null)
        {
            Data = data;
            if (newScheduleHolder != null)
            {
                NewScheduleHolder = newScheduleHolder;
            }
        }
    }
}
