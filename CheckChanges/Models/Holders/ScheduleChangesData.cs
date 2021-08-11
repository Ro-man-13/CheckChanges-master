using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckChanges.Models.Holders
{
    class ScheduleChangesData
    {
        public string Data { get; set; }
        public List<ScheduleChangesHolder> ScheduleChangesHolder { get; set; }

        public ScheduleChangesData()
        {
            Data = string.Empty;
            ScheduleChangesHolder = null;
        }

        public ScheduleChangesData(string data, List<ScheduleChangesHolder> scheduleChangesHolder = null)
        {
            Data = data;
            if (scheduleChangesHolder != null)
            {
                ScheduleChangesHolder = scheduleChangesHolder;
            }
        }
    }
}
