using CheckChanges.Models;
using CheckChanges.Models.Entities;
using CheckChanges.Models.Holders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckChanges.Logic
{
    class NewCasesService
    {
        private readonly nemo_freshEntities db;

        public NewCasesService(nemo_freshEntities db)
        {
            this.db = db;
        }

        public List<NewCasesHolder> GetNewCases(int state)
        {
            Console.WriteLine("---New Cases---");

            List<NewCasesHolder> cases = new List<NewCasesHolder>();
            string sql = "";

            if (state == 1)
            {
                sql = GetInventionsNewCases();
            }
            else if (state == 2)
            {
                sql =  GetTMNewCases();
            }

            var links = db.Database.SqlQuery<NewCasesHolder>(sql);

            foreach (var c in links)
            {
                if (c.User_Cr_Date == MainService.Day)
                {
                    Console.WriteLine(c.OurCase);
                    cases.Add(c);
                }
            }

            return cases;
        }

        private string GetInventionsNewCases()
        {
            string sql = "SELECT DISTINCT Inventions.[Our case] as OurCase, Inventions.[User] as Creator, Inventions.[User_Cr_Date], 1 as Flag " +
                "FROM Inventions " +
                "INNER JOIN [About partner] ON Inventions.[Attorney ID] = [About partner].[Attorney ID] " +
                "LEFT JOIN Shedule ON Inventions.[Our case] = Shedule.[Our case] " +
                "INNER JOIN SheduleConsts ON SheduleConsts.[Action] = Shedule.[Action] AND Shedule.[Status] != SheduleConsts.[Status] " +
                "WHERE 1=1 AND [Applicant ID] is null AND Shedule.ToShow = 0 ";
            string tmp = MainService._casesCriteria;
            string invStr = tmp.Replace("[Attorney ID]", "Inventions.[Attorney ID]");
            return sql + " AND " + invStr;
        }

        private string GetTMNewCases()
        {
            string sql = "SELECT DISTINCT TrademarksIndex.[Our case] as OurCase, TrademarksIndex.[User] as Creator, TrademarksIndex.[User_Cr_Date], 2 as Flag " +
                "FROM TrademarksIndex " +
                "INNER JOIN [About partner] ON TrademarksIndex.[Attorney ID] = [About partner].[Attorney ID] " +
                "LEFT JOIN Shedule ON TrademarksIndex.[Our case] = Shedule.[Our case] " +
                "INNER JOIN SheduleConsts ON SheduleConsts.[Action] = Shedule.[Action] AND Shedule.[Status] != SheduleConsts.[Status] " +
                "WHERE 1=1 AND [Applicant ID] is null AND Shedule.ToShow = 0 ";
            string tmp = MainService._casesCriteria;
            string tmStr = tmp.Replace("[Attorney ID]", "TrademarksIndex.[Attorney ID]");
            return sql + " AND " + tmStr;
        }
    }
}
