using CheckChanges.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckChanges.Logic
{
    class GetUsersCheckChanges
    {
        private readonly nemo_freshEntities db;

        public GetUsersCheckChanges(nemo_freshEntities db)
        {
            this.db = db;
        }

        public List<Users> GetUsers(int type)
        {
            var userCheckCh = db.UsersCheckChanges.ToList();
            List<Users> userList = new List<Users>();
            if (type == 1) {
                userList = userCheckCh.Where(x => x.IsInv == true).Select(x=>x.Users).ToList();
            }
            if (type == 2) {
                userList = userCheckCh.Where(x => x.IsTM == true).Select(x => x.Users).ToList();
            }          
            return userList;
        }

    }
}
