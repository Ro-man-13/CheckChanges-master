using CheckChanges.Models;
using CheckChanges.Models.Entities;
using CheckChanges.Models.Holders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckChanges.Logic
{
    public class BrokenLinksService
    {
        private readonly nemo_freshEntities db;

        public BrokenLinksService(nemo_freshEntities db)
        {
            this.db = db;
        }

        public List<BrokenLinksHolder> GetBrokenLinks(int state)
        {
            Console.WriteLine("---Broken Links---");

            List<BrokenLinksHolder> brokenLinks = new List<BrokenLinksHolder>();
            string sql = "";
            if (state == 1)
                sql = GetInventionsBrokenLinks();
            else if (state == 2)
                sql = GetTMBrokenLinks();

            var links = db.Database.SqlQuery<BrokenLinksHolder>(sql);

            foreach (var inv in links)
            {
                string action = inv.Content;
                
                try
                {
                    string link = inv.File.Replace("#", "").ToLower().Replace("z:", @"\\samson\ip-all").Replace("y:", @"\\samson\info");
                    if (Path.GetExtension(link).Contains(".pdf"))
                    {
                        if (!File.Exists(link))
                        {
                            brokenLinks.Add(new BrokenLinksHolder(inv.OurCase, inv.Content, link, inv.Flag));
                            Console.WriteLine(inv.OurCase);
                        }
                        string source;

                        if (!inv.InOut ?? false)
                        {
                            if (action.EndsWith("."))
                            {
                                action = action.Substring(0, action.Length - 1);
                                source = "Schedule";
                            }
                            else
                                source = "Mail In";

                        }
                        else
                            source = "Mail Out";

                        MainService.SignsList.Add(inv.OurCase);
                        MainService.SignsList.Add(action);
                        MainService.SignsList.Add(source);
                        MainService.SignsList.Add(inv.Flag.ToString());
                        MainService.SignsList.Add(inv.IgnoreDescription.ToString());
                    }
                }
                catch (Exception)
                {
                    brokenLinks.Add(new BrokenLinksHolder(inv.OurCase, inv.Content, "Error! Something wrong with link! Check this case in a manual mode, please.", inv.Flag));
                    Console.WriteLine(inv.OurCase);
                }
            }

            return brokenLinks;
        }

        private string GetInventionsBrokenLinks()
        {
            string sql = "SELECT MAIL.[Наш номер] as OurCase, MAIL.[Содержание] as Content, IsNull([MAIL].[File_Reply2],IsNull([MAIL].[File_Reply],[MAIL].[File])) AS [File], 1 as Flag, MAIL.[IN/OUT] as InOut, MAIL.[IgnoreDescription] " +
                "FROM MAIL " +
                "INNER JOIN (Inventions INNER JOIN [About partner] ON Inventions.[Attorney ID] = [About partner].[Attorney ID]) ON MAIL.[Наш номер] = Inventions.[Our case] " +
                "WHERE 1=1";

            string tmp = MainService._casesCriteria;
            string mailStr = tmp.Replace("AND [", "AND MAIL.[ToShow]=0 AND MAIL.[File] Is Not Null AND MAIL.[File] <>'##' AND MAIL.[Содержание]<>'notif-correct.' AND MAIL.[Содержание]<>'notif-correct' AND [");
            string invStr = mailStr.Replace("NotToShow", "Inventions.[NotToShow]").Replace("[Attorney ID]", "Inventions.[Attorney ID]");

            return sql + " AND " + invStr;
        }

        private string GetTMBrokenLinks()
        {
            List<BrokenLinksHolder> brokenLinks = new List<BrokenLinksHolder>();
            string sql = "SELECT MAIL.[Наш номер] as OurCase, MAIL.[Содержание] as Content, IsNull([MAIL].[File_Reply2],IsNull([MAIL].[File_Reply],[MAIL].[File])) AS [File], 2 as Flag, MAIL.[IN/OUT] as InOut, MAIL.[IgnoreDescription] " +
                "FROM MAIL " +
                "INNER JOIN (TrademarksIndex INNER JOIN [About partner] ON TrademarksIndex.[Attorney ID] = [About partner].[Attorney ID]) ON MAIL.[Наш номер] = TrademarksIndex.[Our case] " +
                "WHERE 1=1";

            string tmp = MainService._casesCriteria;
            string mailStr = tmp.Replace("AND [", "AND MAIL.[ToShow]=0 AND MAIL.[File] Is Not Null AND MAIL.[File] <>'##' AND MAIL.[Содержание]<>'notif-correct.' AND MAIL.[Содержание]<>'notif-correct' AND [");
            string tmStr = mailStr.Replace("NotToShow", "TrademarksIndex.[NotToShow]").Replace("[Attorney ID]", "TrademarksIndex.[Attorney ID]");

            return sql + " AND " + tmStr;
        }
    }
}
