using CheckChanges.Models.Entities;
using CheckChanges.Models.Holders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckChanges.Logic
{
    class NewMailService
    {
        private readonly nemo_freshEntities db;

        public NewMailService(nemo_freshEntities db)
        {
            this.db = db;
        }

        public List<NewMailHolder> GetNewMailService(int state)
        {
            Console.WriteLine("---New Mail Records---");

            List<NewMailHolder> newMails = new List<NewMailHolder>();

            string sql = "";

            if (state == 1)
                sql = GetInventionNewMail();
            else if (state == 2)
                sql = GetTMNewMail();

            var mails = db.Database.SqlQuery<NewMailHolder>(sql);

            foreach (var inv in mails)
            {
                for (int j = 0; j < MainService.CasesList.Count; j = j + 2)
                {
                    if (inv.OurCase.ToUpper().Contains(MainService.CasesList[j]))
                    {
                        Console.WriteLine(inv.OurCase);
                        newMails.Add(new NewMailHolder(
                            inv.OurCase,
                            inv.Creator,
                            Convert.ToInt32(MainService.CasesList[j + 1]),
                            inv.Content));
                    }
                }
            }
            return newMails;
        }

        private string GetInventionNewMail()
        {

            string sql = "SELECT MAIL.[Наш номер] as OurCase, MAIL.[Creator], MAIL.[Содержание] as Content " +
                "FROM (MAIL INNER JOIN Inventions ON MAIL.[Наш номер] = Inventions.[Our case]) LEFT JOIN TrademarksIndex ON MAIL.[Наш номер] = TrademarksIndex.[Our case] " +
                "WHERE (((MAIL.CreationDate)='" + MainService.DateString + "') " +
                "AND ((MAIL.ToShow)=0) " +
                "AND ((CAST(IsNull(Inventions.[User_Cr_Date],TrademarksIndex.[User_Cr_Date]) AS DATE))<>'" + MainService.DateString + "'" +
                "And (IsNull([Inventions].[User_Cr_Date],[TrademarksIndex].[User_Cr_Date])) Is Not Null)) " +
                "ORDER BY MAIL.RegNo";

            return sql;
        }

        private string GetTMNewMail()
        {
            string sql = "SELECT MAIL.[Наш номер] as OurCase, MAIL.[Creator], MAIL.[Содержание] as Content " +
                "FROM (MAIL INNER JOIN TrademarksIndex ON MAIL.[Наш номер] = TrademarksIndex.[Our case]) LEFT JOIN Inventions ON MAIL.[Наш номер] = Inventions.[Our case] " +
                "WHERE (((MAIL.CreationDate)='" + MainService.DateString + "') " +
                "AND ((MAIL.ToShow)=0) " +
                "AND ((CAST(IsNull(TrademarksIndex.[User_Cr_Date], Inventions.[User_Cr_Date]) AS DATE))<>'" + MainService.DateString + "'" +
                "And (IsNull([TrademarksIndex].[User_Cr_Date], [Inventions].[User_Cr_Date])) Is Not Null)) " +
                "ORDER BY MAIL.RegNo";

            return sql;
        }
    }
}
