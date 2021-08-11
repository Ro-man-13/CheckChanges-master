using CheckChanges.Models.Entities;
using CheckChanges.Models.Holders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckChanges.Logic
{
    class CheckChangesService
    {
        nemo_freshEntities db;

        public CheckChangesService(nemo_freshEntities db)
        {
            this.db = db;
        }

        public List<CheckMatchesHolder> GetCheckMatches(List<string> dataList)
        {
            Console.WriteLine("---Check Matches---");

            List<CheckMatchesHolder> checkMatches = new List<CheckMatchesHolder>();
          
            for (int i = 0; i < dataList.Count; i = i + 5)
            {

                string action = dataList[i + 1];
                if (action.EndsWith("."))
                {
                    action = action.Substring(0, action.Length - 1);
                }

                string sql = "SELECT * FROM DocumentSigns WHERE(((DocumentSigns.[Sign])= '" + action + "'))";

                var query = db.DocumentSigns.SqlQuery(sql).ToList();

                if (query.Count != 0)
                {
                    for (int j = 0; j < query.Count(); j++)
                    {
                        if (query[j].FullDescription != null && query[j].FullDescription.ToString().ToLower() == "filing request")
                        {
                            MainService.DuplicateList.Add(dataList[i]);
                            MainService.DuplicateList.Add(dataList[i + 3]);
                        }
                    }
                }
                else
                {
                    if (!dataList[i + 4].Equals("True"))
                    {
                        Console.WriteLine(dataList[i]);
                        checkMatches.Add(new CheckMatchesHolder(dataList[i], dataList[i + 1], dataList[i + 2],
                            Convert.ToInt32(dataList[i + 3])));
                    }
                }
            }

            return checkMatches;
        }
    }
}
