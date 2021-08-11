using CheckChanges.Models.Entities;
using CheckChanges.Models.Holders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckChanges.Logic
{
    class NewScheduleService
    {
        private readonly nemo_freshEntities db;

        public NewScheduleService(nemo_freshEntities db)
        {
            this.db = db;
        }

        public List<NewScheduleHolder> GetNewSchedule(int state)
        {
            Console.WriteLine("---New Schedule Records---");

            List<NewScheduleHolder> newSchedules = new List<NewScheduleHolder>();
            string sql = "";

            if (state == 1)
                sql = GetInventionNewSchedule();
            else if (state == 2)
                sql = GetTMNewSchedule();

            var schedules = db.Database.SqlQuery<NewScheduleHolder>(sql);

            foreach (var inv in schedules)
            {
                for (int j = 0; j < MainService.CasesList.Count; j = j + 2)
                {
                    if (inv.OurCase.ToUpper().Contains(MainService.CasesList[j]))
                    {
                        Console.WriteLine(inv.OurCase);
                        newSchedules.Add(new NewScheduleHolder(
                            inv.OurCase,
                            inv.Creator,
                            Convert.ToInt32(MainService.CasesList[j + 1]),
                            inv.Action,
                            inv.Status,
                            inv.Show));
                    }
                }
            }

            return newSchedules;
        }

        private string GetInventionNewSchedule()
        {
            string sql = "SELECT Shedule.[Our case] as OurCase, Shedule.Creator, Shedule.Action, Shedule.Status, Shedule.[Show_all] as Show " +
                "FROM (Shedule INNER JOIN Inventions ON Shedule.[Our case] = Inventions.[Our case]) " +
                "WHERE (((Shedule.[MailID])=0) " +
                "AND ((Shedule.[User]) Is Not Null) " +
                "AND (Inventions.[User_Cr_Date] Is Not Null) " +
                "And (CAST(Inventions.[User_Cr_Date] AS DATE)<>'" + MainService.DayEn + "') " +
                "AND ((Shedule.[Creation_date])='" + MainService.DayEn + "')) " +
                "ORDER BY Shedule.[ScheduleID]";
          
            return sql;
        }

        private string GetTMNewSchedule()
        {
            string sql = "SELECT Shedule.[Our case] as OurCase, Shedule.Creator, Shedule.Action, Shedule.Status, Shedule.[Show_all] as Show " +
                "FROM (Shedule INNER JOIN TrademarksIndex ON Shedule.[Our case] = TrademarksIndex.[Our case]) " +
                "WHERE (((Shedule.[MailID])=0) " +
                "AND ((Shedule.[User]) Is Not Null) " +
                "AND (TrademarksIndex.[User_Cr_Date] Is Not Null) " +
                "And (CAST(TrademarksIndex.[User_Cr_Date] AS DATE)<>'" + MainService.DayEn + "') " +
                "AND ((Shedule.[Creation_date])='" + MainService.DayEn + "')) " +
                "ORDER BY Shedule.[ScheduleID]";

            return sql;
        }
    }
}
