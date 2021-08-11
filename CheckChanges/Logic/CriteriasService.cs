using CheckChanges.Models;
using CheckChanges.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckChanges.Logic
{
    public class CriteriasService
    {
        nemo_freshEntities db;

        public CriteriasService(nemo_freshEntities db)
        {
            this.db = db;
        }

        public string GetCasesCriteria(int partnerId)
        {
            nemo_freshEntities db = new nemo_freshEntities();

            string sqlCasesCriteria = "SELECT * FROM PartnerZoneCriteria WHERE (((PartnerZoneCriteria.user_id)=" + partnerId + "))";

            List<Criteria> criterias = db.PartnerZoneCriteria
                .SqlQuery(sqlCasesCriteria)
                .Select(pzc => new Criteria { Name = pzc.WhatCriteria, Value = pzc.ID })
                .ToList();

            StringBuilder sb = new StringBuilder();
            bool first = true;
            foreach (Criteria criteria in criterias)
            {
                if (first)
                {
                    sb.Append("Closed=0 AND NotToShow=0 AND [" + criteria.Name + "]=");
                    sb.Append("'" + criteria.Value + "'");
                    first = false;
                }
                else
                {
                    sb.Append(" Or Closed=0 AND NotToShow=0 AND [" + criteria.Name + "]=");
                    sb.Append("'" + criteria.Value + "'");
                }
            }

            return sb.ToString();
        }      
    }
}
