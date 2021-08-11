using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckChanges.Models
{
    class CasesModel
    {
        public string OurCase { get; set; }
        public int Flag { get; set; }
        public string User { get; set; }
        public DateTime? User_Cr_Date { get; set; }
    }
}
