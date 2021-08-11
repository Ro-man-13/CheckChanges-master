using CheckChanges.Models.Entities;
using CheckChanges.Models.Holders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckChanges.Logic
{
    class NotMatchingRecordsService
    {
        nemo_freshEntities db;

        public NotMatchingRecordsService(nemo_freshEntities db)
        {
            this.db = db;
        }

        public List<NotMatchingRecordsHolder> GetNotMatchingRecords(int state)
        {
            Console.WriteLine("---Not Matching Records---");

            List<NotMatchingRecordsHolder> notMatchingRecords = new List<NotMatchingRecordsHolder>();

            string sql = "";

            if (state == 1)
                sql = GetInventionsNotMatching();
            else if (state == 2)
                sql = GetTMNotMatching();

            var query = db.Database.SqlQuery<NotMatchingRecordsHolder>(sql);

            foreach (var r in query)
            {
                if (!String.Equals(r.OurSheduleCase.ToString(), r.OurMailCase.ToString(), StringComparison.CurrentCultureIgnoreCase))
                {
                    for (int j = 0; j < MainService.CasesList.Count; j = j + 2)
                    {
                        if (r.OurSheduleCase.ToUpper().Contains(MainService.CasesList[j]))
                        {
                            notMatchingRecords.Add(r);
                        }
                    }
                }

            }

            return notMatchingRecords;
        }

        private string GetInventionsNotMatching()
        {
            string sql = "SELECT MAIL.[Наш номер], Shedule.[Our case], MAIL.Дата, Shedule.Creation_date, MAIL.RegNo " +
                "FROM Shedule " +
                "INNER JOIN MAIL ON Shedule.MailID = MAIL.RegNo " +
                "INNER JOIN Inventions ON Shedule.[Our case] = Inventions.[Our case]";

            return sql;
        }

        private string GetTMNotMatching()
        {
            string sql = "SELECT MAIL.[Наш номер], Shedule.[Our case], MAIL.Дата, Shedule.Creation_date, MAIL.RegNo FROM Shedule INNER JOIN MAIL ON Shedule.MailID = MAIL.RegNo INNER JOIN TrademarksIndex ON Shedule.[Our case] = TrademarksIndex.[Our case]";

            return sql;
        }
    }
}
