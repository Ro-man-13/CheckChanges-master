using CheckChanges.Models;
using CheckChanges.Models.Entities;
using CheckChanges.Models.Holders;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckChanges.Logic
{
    class MainService
    {
        public static string _casesCriteria;
        public static DateTime Day;
        public static DateTime DayEn;

        public static List<string> SignsList = new List<string>();
        public static List<string> CasesList = new List<string>();
        public static List<FileStorageHolder> fileStorage = new List<FileStorageHolder>();
        public static List<string> DuplicateList = new List<string>();

        public static string DateString = string.Empty;
        public static string EnteredDate = string.Empty;     

        private static readonly nemo_freshEntities db = new nemo_freshEntities();
        private static List<GlobalDataHolder> DataInv = new List<GlobalDataHolder>();
        private static List<GlobalDataHolder> DataTM = new List<GlobalDataHolder>();
        private static BrokenLinksService brokenLinks = new BrokenLinksService(db);
        private static CaseChangesService caseChanges = new CaseChangesService(db);
        private static CheckChangesService checkChanges = new CheckChangesService(db);
        private static ClientFolderService clientFolder = new ClientFolderService(db);
        private static CriteriasService criterias = new CriteriasService(db);
        private static DuplicateDescriptionService duplicateDescription = new DuplicateDescriptionService(db);
        private static MailChangesService mailChanges = new MailChangesService(db);
        private static NewCasesService newCases = new NewCasesService(db);
        private static NewMailService newMail = new NewMailService(db);
        private static NewScheduleService newSchedule = new NewScheduleService(db);
        private static NotMatchingRecordsService notMatching = new NotMatchingRecordsService(db);
        private static ScheduleChangesService scheduleChanges = new ScheduleChangesService(db);
        private static SearchFilesService searchFiles = new SearchFilesService(db);
        private static GetUsersCheckChanges UsersCheckChanges = new GetUsersCheckChanges(db);

        public static void StartProgram(string dateString, string enteredDate)
        {
            DateString = dateString;
            EnteredDate = enteredDate;
            Day = DateTime.Parse(dateString);
            DayEn = DateTime.Parse(enteredDate);

            ClientsList(DateString, EnteredDate);        

            MailingService.CreateMail(DataInv, 1, GetUser(1));
            MailingService.CreateMail(DataTM, 2, GetUser(2));
        }

     

        private static void GetCases(string partner, int partnerId, int userGroup, int state)
        {
            DbRawSqlQuery<CasesModel> query = null;
            if (state == 0)
            {
                GetCases(partner, partnerId, userGroup, 1);
                GetCases(partner, partnerId, userGroup, 2);
            }
            else if (state == 1)
            {
                query = GetInventionsCases();

                CasesList = new List<string>();

                foreach (var item in query)
                {
                    CasesList.Add(item.OurCase);
                    CasesList.Add(item.Flag.ToString());
                }
                clientFolder.GetClientFolder(partnerId);

                DataInv.Add(new GlobalDataHolder(partner, partnerId, userGroup,
                new NewCasesData("New Cases", newCases.GetNewCases(state)),
                new NewScheduleData("New Schedule Records", newSchedule.GetNewSchedule(state)),
                new NewMailData("New Mail Records", newMail.GetNewMailService(state)),
                new FileStorageData("New Files in File Storage", fileStorage),
                new CasesChangesData("Changes In Cases Records", caseChanges.GetCaseChanges(state)),
                new ScheduleChangesData("Changes In Schedule Records", scheduleChanges.GetScheduleChanges(state)),
                new MailChangesData("Changes In Mail Records", mailChanges.GetMailChanges(state)),
                new NotMatchingRecordsData("Not Matching Records", notMatching.GetNotMatchingRecords(state)),
                new BrokenLinksData("Broken Links", brokenLinks.GetBrokenLinks(state)),
                new CheckMatchesData("Check Matches Finder", checkChanges.GetCheckMatches(SignsList)),
                new LostFilesData("Lost Files", searchFiles.GetSchedules(state, partnerId)),
                new DuplicateData("Filing Request Finder", duplicateDescription.GetDuplicateDescription())
                ));
            }
            else if (state == 2)
            {
                query = GetTMCases();

                CasesList = new List<string>();

                foreach (var item in query)
                {
                    CasesList.Add(item.OurCase);
                    CasesList.Add(item.Flag.ToString());
                }
                clientFolder.GetClientFolder(partnerId);

                DataTM.Add(new GlobalDataHolder(partner, partnerId, userGroup,
                new NewCasesData("New Cases", newCases.GetNewCases(state)),
                new NewScheduleData("New Schedule Records", newSchedule.GetNewSchedule(state)),
                new NewMailData("New Mail Records", newMail.GetNewMailService(state)),
                new FileStorageData("New Files in File Storage", fileStorage),
                new CasesChangesData("Changes In Cases Records", caseChanges.GetCaseChanges(state)),
                new ScheduleChangesData("Changes In Schedule Records", scheduleChanges.GetScheduleChanges(state)),
                new MailChangesData("Changes In Mail Records", mailChanges.GetMailChanges(state)),
                new NotMatchingRecordsData("Not Matching Records", notMatching.GetNotMatchingRecords(state)),
                new BrokenLinksData("Broken Links", brokenLinks.GetBrokenLinks(state)),
                new CheckMatchesData("Check Matches Finder", checkChanges.GetCheckMatches(SignsList)),
                new LostFilesData("Lost Files", searchFiles.GetSchedules(state, partnerId)),
                new DuplicateData("Filing Request Finder", duplicateDescription.GetDuplicateDescription())
                ));
            }

            SignsList = new List<string>();
            DuplicateList = new List<string>();
            fileStorage = new List<FileStorageHolder>();
        }

        public static List<Users> GetUser(int type) {
           return UsersCheckChanges.GetUsers(type);
        }

     private static DbRawSqlQuery<CasesModel> GetInventionsCases()
        {
            string InvLinks = "SELECT " +
                "Inventions.[Our case] AS OurCase, " +
                "Inventions.[User], " +
                "Inventions.[User_Cr_Date], " +
                "1 AS Flag " +
                "FROM Inventions INNER JOIN [About partner] ON Inventions.[Attorney ID] = [About partner].[Attorney ID] WHERE 1=1 ";

            string InvStr = _casesCriteria.Replace("[Attorney ID]", "Inventions.[Attorney ID]");

            return db.Database.SqlQuery<CasesModel>(InvLinks + " AND " + InvStr);
        }

        private static DbRawSqlQuery<CasesModel> GetTMCases()
        {
            string TMLinks = "SELECT " +
                "TrademarksIndex.[Our case] AS OurCase, " +
                "TrademarksIndex.[User], " +
                "TrademarksIndex.[User_Cr_Date], " +
                "2 AS Flag " +
                "FROM TrademarksIndex INNER JOIN [About partner] ON TrademarksIndex.[Attorney ID] = [About partner].[Attorney ID] WHERE 1=1 ";

            string TMStr = _casesCriteria.Replace("[Attorney ID]", "TrademarksIndex.[Attorney ID]");

            return db.Database.SqlQuery<CasesModel>(TMLinks + " AND " + TMStr);
        }

        private static void ClientsList(string dayRu, string dayEn)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var query = db.Users.Where(user => (user.UserGroup_id == 10 || user.UserGroup_id == 18 || user.UserGroup_id == 27) && user.IsActive == true).OrderBy(user => user.Username);

            foreach (var user in query)
            {
                int state;
                bool showAll = (bool)user.ShowTM;
                bool showTm = (bool)user.ShowOnlyTM;
                if (showAll)
                {
                    state = 0;
                }
                else if (showTm)
                {
                    state = 2;
                }
                else
                {
                    state = 1;
                }

                Console.WriteLine("-----//-----" + user.Username + "-----//-----");

                _casesCriteria = criterias.GetCasesCriteria(user.id_User);
                GetCases(user.Username, user.id_User, (int)user.UserGroup_id, state);

            }
            stopwatch.Stop();
            TimeSpan timeSpan = stopwatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
        }
    }
}
