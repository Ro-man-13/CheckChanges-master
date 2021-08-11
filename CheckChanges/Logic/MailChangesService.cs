using CheckChanges.Models.Entities;
using CheckChanges.Models.Holders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckChanges.Logic
{
    class MailChangesService
    {
        private readonly nemo_freshEntities db;

        public MailChangesService(nemo_freshEntities db)
        {
            this.db = db;
        }

        public List<MailChangesHolder> GetMailChanges(int state)
        {
            Console.WriteLine("---Changes In Mail Records---");

            List<MailChangesHolder> mailChanges = new List<MailChangesHolder>();

            string sql = "";

            if (state == 1)
                sql = GetInventionsMailChanges();
            else if (state == 2)
                sql = GetTMMailChanges();

            var mails = db.Database.SqlQuery<MailChangesHolder>(sql);

            foreach (var mail in mails)
            {
                for (int j = 0; j < MainService.CasesList.Count; j = j + 2)
                {
                    if (mail.OurCase.ToString().ToUpper().Contains(MainService.CasesList[j]))
                    {
                        Console.WriteLine(mail.ChangedBy);
                        mailChanges.Add(new MailChangesHolder(
                            mail.OurCase,
                            mail.ChangedBy,
                            Convert.ToInt32(MainService.CasesList[j + 1])));
                    }
                }
            }
            return mailChanges;
        }

        private string GetInventionsMailChanges()
        {

            string sql = "SELECT DISTINCT MailChangedLog.OurCase, MailChangedLog.ChangeBy " +
                         "FROM (MailChangedLog INNER JOIN Inventions ON MailChangedLog.OurCase = Inventions.[Our case]) LEFT JOIN TrademarksIndex ON MailChangedLog.OurCase = TrademarksIndex.[Our case] " +
                         "WHERE (((IsNull([Inventions].[User_Cr_Date],[TrademarksIndex].[User_Cr_Date])) Is Not Null) " +
                         "And ((CAST(IsNull(Inventions.[User_Cr_Date],TrademarksIndex.[User_Cr_Date]) AS DATE))<>'" + MainService.DateString + "') " +
                         "AND ((CAST(MailChangedLog.[ExactTime] AS DATE))='" + MainService.DateString + "'))";

            return sql;
        }

        private string GetTMMailChanges()
        {
            string sql = "SELECT DISTINCT MailChangedLog.OurCase, MailChangedLog.ChangeBy " +
                         "FROM (MailChangedLog INNER JOIN TrademarksIndex ON MailChangedLog.OurCase = TrademarksIndex.[Our case] LEFT JOIN Inventions ON MailChangedLog.OurCase = Inventions.[Our case]) " +
                         "WHERE (((IsNull([TrademarksIndex].[User_Cr_Date], [Inventions].[User_Cr_Date])) Is Not Null) " +
                         "And ((CAST(IsNull(TrademarksIndex.[User_Cr_Date], Inventions.[User_Cr_Date]) AS DATE))<>'" + MainService.DateString + "') " +
                         "AND ((CAST(MailChangedLog.[ExactTime] AS DATE))='" + MainService.DateString + "'))";

            return sql;
        }
    }
}
