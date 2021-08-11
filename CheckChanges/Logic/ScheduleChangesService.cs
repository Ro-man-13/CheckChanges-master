using CheckChanges.Models.Entities;
using CheckChanges.Models.Holders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckChanges.Logic
{
    class ScheduleChangesService
    {
        private readonly nemo_freshEntities db;

        public ScheduleChangesService(nemo_freshEntities db)
        {
            this.db = db;
        }

        public List<ScheduleChangesHolder> GetScheduleChanges(int state)
        {
            Console.WriteLine("---Changes In Schedule Records---");

            List<ScheduleChangesHolder> scheduleChanges = new List<ScheduleChangesHolder>();

            string sql = "";

            if (state == 1)
                sql = GetInventionsScheduleChanges();
            else if (state == 2)
                sql = GetTMScheduleChanges();

            var schedules = db.Database.SqlQuery<ScheduleChangesHolder>(sql);

            foreach (var sch in schedules)
            {
                for (int j = 0; j < MainService.CasesList.Count; j = j + 2)
                {
                    if (sch.OurCase.ToString().ToUpper().Contains(MainService.CasesList[j]))
                    {
                        Console.WriteLine(sch.ChangedBy);
                        scheduleChanges.Add(new ScheduleChangesHolder(
                            sch.OurCase,
                            sch.ChangedBy,
                            Convert.ToInt32(MainService.CasesList[j + 1])));
                    }
                }
            }
            return scheduleChanges;
        }

        private string GetInventionsScheduleChanges()
        {

            string sql = "SELECT DISTINCT ScheduleClientChangesLog.OurCase, ScheduleClientChangesLog.ChangedBy " +
                "FROM (ScheduleClientChangesLog INNER JOIN Inventions ON ScheduleClientChangesLog.OurCase = Inventions.[Our case]) LEFT JOIN TrademarksIndex ON ScheduleClientChangesLog.OurCase = TrademarksIndex.[Our case] " +
                "WHERE (((IsNull([Inventions].[User_Cr_Date],[TrademarksIndex].[User_Cr_Date])) Is Not Null) " +
                "And ((CAST(IsNull(Inventions.[User_Cr_Date],TrademarksIndex.[User_Cr_Date]) AS DATE))<>'" + MainService.DateString + "') " +
                "AND ((CAST([ScheduleClientChangesLog].[ExactTime] AS DATE))='" + MainService.DateString + "'))";

            return sql;
        }

        private string GetTMScheduleChanges()
        {
            string sql = "SELECT DISTINCT ScheduleClientChangesLog.OurCase, ScheduleClientChangesLog.ChangedBy " +
                "FROM (ScheduleClientChangesLog INNER JOIN TrademarksIndex ON ScheduleClientChangesLog.OurCase = TrademarksIndex.[Our case]) LEFT JOIN Inventions ON ScheduleClientChangesLog.OurCase = Inventions.[Our case] " +
                "WHERE (((IsNull([TrademarksIndex].[User_Cr_Date], [Inventions].[User_Cr_Date])) Is Not Null) " +
                "And ((CAST(IsNull(TrademarksIndex.[User_Cr_Date], Inventions.[User_Cr_Date]) AS DATE))<>'" + MainService.DateString + "') " +
                "AND ((CAST([ScheduleClientChangesLog].[ExactTime] AS DATE))='" + MainService.DateString + "'))";

            return sql;
        }
    }
}
