using CheckChanges.Models.Entities;
using CheckChanges.Models.Holders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckChanges.Logic
{
    class DuplicateDescriptionService
    {
        private readonly nemo_freshEntities db;

        public DuplicateDescriptionService(nemo_freshEntities db)
        {
            this.db = db;
        }

        public List<DuplicateHolder> GetDuplicateDescription()
        {
            List<DuplicateHolder> duplicateHolder = new List<DuplicateHolder>();
            List<string> tmpList = new List<string>();
            for (int i = 0; i < MainService.DuplicateList.Count; i = i + 2)
            {
                for (int j = i; j < MainService.DuplicateList.Count; j = j + 2)
                {
                    if (i != j)
                    {
                        if (MainService.DuplicateList[i].Equals(MainService.DuplicateList[j]))
                        {
                            tmpList.Add(MainService.DuplicateList[i].Replace("-", "").Replace("/", "").ToLower());
                        }
                    }
                }
            }
            IEnumerable<string> distinctTmp = tmpList.Distinct();
            Console.WriteLine("---FilingRequestFinder---");
            foreach (var tmp in distinctTmp)
            {
                for (int k = 0; k < MainService.CasesList.Count; k = k + 2)
                {
                    if (tmp.Equals(MainService.CasesList[k].Replace("-", "").Replace("/", "").ToLower()))
                    {
                        duplicateHolder.Add(new DuplicateHolder(MainService.CasesList[k], Convert.ToInt32(MainService.CasesList[k + 1])));
                        Console.WriteLine(MainService.CasesList[k]);
                    }
                }
            }
            return duplicateHolder;
        }
    }
}
