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
    class CaseChangesService
    {
        readonly private nemo_freshEntities db;

        public CaseChangesService(nemo_freshEntities db)
        {
            this.db = db;
        }

        public List<CasesChangesHolder> GetCaseChanges(int state)
        {
            Console.WriteLine("---Changes in Cases Records---");

            List<CasesChangesHolder> casesChanges = new List<CasesChangesHolder>();

            string sql = "";

            if (state == 1)
                sql = GetInventionsCaseChanges();
            else if (state == 2)
                sql = GetTMCaseChanges();

            var links = db.Database.SqlQuery<CasesChangesHolder>(sql);

            foreach (var inv in links)
            {
                if (inv.User_Cr_Date != MainService.Day && inv.Last_User_Date == MainService.Day)
                {
                    Console.WriteLine(inv.OurCase);
                    casesChanges.Add(inv);
                }
            }

            return casesChanges;
        }

        private string GetInventionsCaseChanges()
        {          
            string sql = "SELECT [Our case] as OurCase, Last_User, 1 as Flag, User_Cr_Date, Last_User_Date " +
                "FROM Inventions " +
                "INNER JOIN [About partner] ON Inventions.[Attorney ID] = [About partner].[Attorney ID] " +
                "WHERE 1=1 ";
            string tmp = MainService._casesCriteria;
            string invStr = tmp.Replace("[Attorney ID]", "Inventions.[Attorney ID]");

            if (invStr != "")
                return sql + " AND " + invStr;
            else
                return sql;
        }

        private string GetTMCaseChanges()
        {
            string sql = "SELECT [Our case] as OurCase, Last_User, 2 as Flag, User_Cr_Date, Last_User_Date " +
                "FROM TrademarksIndex " +
                "INNER JOIN [About partner] ON TrademarksIndex.[Attorney ID] = [About partner].[Attorney ID] " +
                "WHERE 1=1 ";
            string tmp = MainService._casesCriteria;
            string tmStr = tmp.Replace("[Attorney ID]", "TrademarksIndex.[Attorney ID]");

            if (tmStr != "")
                return sql + " AND " + tmStr;
            else
                return sql;
        }
    }
}
