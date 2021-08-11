using CheckChanges.Models;
using CheckChanges.Models.Entities;
using CheckChanges.Models.Holders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CheckChanges.Logic
{
    class SearchFilesService
    {
        private readonly nemo_freshEntities db;

        public SearchFilesService(nemo_freshEntities db)
        {
            this.db = db;
        }
        public List<LostFilesHolder> GetSchedules(int state, int partnerId)
        {
            Console.WriteLine("---Check files---");
            List<LostFilesHolder> lostFiles = new List<LostFilesHolder>();
            string sql = "";
            for (int k = 0; k < MainService.CasesList.Count; k = k + 2)
            {
                if (state == 1)
                    sql = GetInventionSchedule(MainService.CasesList[k]);
                else if (state == 2)
                    sql = GetTMSchedule(MainService.CasesList[k]);

                var schedules = db.Database.SqlQuery<ScheduleModel>(sql);

                foreach (var sched in schedules)
                {
                    List<string> docFolderList = new List<string>();

                    if (sched.Action.ToLower() == "filing" && sched.Status.ToLower().Contains("application filed"))
                    {
                        docFolderList = GetPartnerDocs(partnerId);
                        foreach (string s in docFolderList)
                        {
                            var list = CheckFiling(s, sched.Our_case, state);
                            lostFiles.AddRange(list);
                        }
                    }
                    if ((sched.Action.ToLower() == "decision" || sched.Action.ToLower() == "grant") && sched.Status.ToLower().Contains("decision reported"))
                    {
                        docFolderList = GetPartnerDocs(partnerId);
                        foreach (string s in docFolderList)
                        {
                            var list = CheckDecisionGrant(s, sched.Our_case, state);
                            lostFiles.AddRange(list);
                        }
                    }
                    if (sched.Action.ToLower() == "patent" && sched.Status.ToLower().Contains("patent reported"))
                    {
                        docFolderList = GetPartnerDocs(partnerId);
                        foreach (string s in docFolderList)
                        {
                            var list = CheckPatent(s, sched.Our_case, state);
                            lostFiles.AddRange(list);
                        }
                    }
                }
            }


            return lostFiles;
        }

        private List<LostFilesHolder> CheckFiling(string sDir, string ourCase, int state)
        {
            List<LostFilesHolder> filingFiles = new List<LostFilesHolder>();
            if (sDir != null)
            {
                foreach (string d in Directory.GetDirectories(sDir).Where(dir => dir.Contains(Regex.Replace(ourCase, @"[^\d]+", ""))))
                {
                    string tmp = Directory.GetDirectories(d).Where(dir => dir.Contains("f-rep")).FirstOrDefault();
                    if (tmp != null)
                    {
                        try
                        {
                            var files = Directory.GetFiles(tmp, "*.pdf");
                            if (files.Where(f => f.Contains("receipt")).Count() == 0)
                                filingFiles.Add(new LostFilesHolder(ourCase, tmp, "receipt.pdf", state));
                            if (files.Where(f => f.Contains("request")).Count() == 0)
                                filingFiles.Add(new LostFilesHolder(ourCase, tmp, "request.pdf", state));
                            if (state == 1)
                            {
                                if (files.Where(f => f.Contains("spec")).Count() == 0)
                                    filingFiles.Add(new LostFilesHolder(ourCase, tmp, "spec.pdf", state));
                                if (files.Where(f => f.Contains("clms")).Count() == 0)
                                    filingFiles.Add(new LostFilesHolder(ourCase, tmp, "clms.pdf", state));
                                if (files.Where(f => f.Contains("figs")).Count() == 0)
                                    filingFiles.Add(new LostFilesHolder(ourCase, tmp, "figs.pdf", state));
                                if (files.Where(f => f.Contains("ua.pdf")).Count() == 0)
                                    filingFiles.Add(new LostFilesHolder(ourCase, tmp, "ua.pdf", state));
                            }
                        }
                        catch { }
                    }
                    else
                        CheckFiling(d, ourCase, state);
                }
            }
            return filingFiles;
        }

        private List<LostFilesHolder> CheckDecisionGrant(string sDir, string ourCase, int state)
        {
            List<LostFilesHolder> dgFiles = new List<LostFilesHolder>();
            if (sDir != null)
            {
                foreach (string d in Directory.GetDirectories(sDir).Where(dir => dir.Contains(Regex.Replace(ourCase, @"[^\d]+", ""))))
                {
                    string tmp = Directory.GetDirectories(d).Where(dir => dir.Contains("f-rep")).FirstOrDefault();
                    if (tmp != null)
                    {
                        try
                        {
                            var files = Directory.GetFiles(tmp);
                            if (files.Where(f => f.Contains("gr-tr")).Count() == 0)
                                dgFiles.Add(new LostFilesHolder(ourCase, tmp, "gr-tr", state));  
                        }
                        catch { }
                    }
                    else
                        CheckDecisionGrant(d, ourCase, state);
                }
            }
            return dgFiles;
        }

        private List<LostFilesHolder> CheckPatent(string sDir, string ourCase, int state)
        {
            List<LostFilesHolder> patentFiles = new List<LostFilesHolder>();
            if (sDir != null)
            {
                foreach (string d in Directory.GetDirectories(sDir).Where(dir => dir.Contains(Regex.Replace(ourCase, @"[^\d]+", ""))))
                {
                    string tmp = Directory.GetDirectories(d).Where(dir => dir.Contains("f-rep")).FirstOrDefault();
                    if (tmp != null)
                    {
                        try
                        {
                            var files = Directory.GetFiles(tmp);
                            if (files.Where(f => f.Contains("pat-tr")).Count() == 0)
                                patentFiles.Add(new LostFilesHolder(ourCase, tmp, "pat-tr", state));
                            if (files.Where(f => f.Contains("patent copy")).Count() == 0)
                                patentFiles.Add(new LostFilesHolder(ourCase, tmp, "patent copy", state));
                        }
                        catch { }
                    }
                    else
                        CheckPatent(d, ourCase, state);
                }
            }
            return patentFiles;
        }

        private List<string> GetPartnerDocs(int partnerId)
        {
            List<string> docFoldersList = new List<string>();
            string docs = "SELECT DocumentFolders.DocumentFolder FROM DocumentFolders WHERE (((DocumentFolders.client_id)=" + partnerId + "))";
            var docFolder = db.Database.SqlQuery<DocumentFold>(docs);
            foreach (var doc in docFolder)
            {
                string path = @"\\samson\ip-all\--MVI\" + doc.DocumentFolder;
                if (!Directory.Exists(path))
                {
                    path = @"\\samson\ip-all\--MVI\UKRAINE\" + doc.DocumentFolder;
                }
                docFoldersList.Add(!Directory.Exists(path) ? null : path);
            }
            return docFoldersList;
        }
        private string GetInventionSchedule(string ourCase)
        {
            string sql = "SELECT Shedule.[Our case] as Our_case, Shedule.Creator, Shedule.Action, Shedule.Status, Shedule.[Show_all]" +
                "FROM (Shedule INNER JOIN Inventions ON Shedule.[Our case] = Inventions.[Our case]) " +
                "WHERE (Shedule.[Our case] = '" + ourCase + "') " +
                "AND Action is not null " +
                "AND Status is not null " +
                "AND ((Shedule.[MailID])=0) " +
                "AND ((Shedule.[User]) Is Not Null) " +
                "AND (Inventions.[User_Cr_Date] Is Not Null) " +
                "ORDER BY Shedule.[ScheduleID]";

            return sql;
        }

        private string GetTMSchedule(string ourCase)
        {
            string sql = "SELECT Shedule.[Our case] as Our_case, Shedule.Creator, Shedule.Action, Shedule.Status, Shedule.[Show_all]" +
                "FROM (Shedule INNER JOIN TrademarksIndex ON Shedule.[Our case] = TrademarksIndex.[Our case]) " +
                "WHERE (Shedule.[Our case] = '" + ourCase + "') " +
                "AND Action is not null " +
                "AND Status is not null " +
                "AND ((Shedule.[MailID])=0) " +
                "AND ((Shedule.[User]) Is Not Null) " +
                "AND (TrademarksIndex.[User_Cr_Date] Is Not Null) " +
                "ORDER BY Shedule.[ScheduleID]";

            return sql;
        }
    }
}
