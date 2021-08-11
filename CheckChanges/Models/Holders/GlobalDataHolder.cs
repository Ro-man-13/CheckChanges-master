using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckChanges.Models.Holders
{
    class GlobalDataHolder
    {
        public string PartnerName { get; set; }
        public int PartnerId { get; set; }
        public int UserGroup { get; set; }
        public NewCasesData CasesData { get; set; }
        public NewScheduleData ScheduleData { get; set; }
        public NewMailData MailData { get; set; }
        public FileStorageData FileStorageData { get; set; }
        public CasesChangesData CasesChangesData { get; set; }
        public ScheduleChangesData ScheduleChangesData { get; set; }
        public MailChangesData MailChangesData { get; set; }
        public NotMatchingRecordsData NotMatchingRecordsData { get; set; }
        public BrokenLinksData BrokenLinksData { get; set; }
        public CheckMatchesData CheckMatchesData { get; set; }
        public LostFilesData LostFilesData { get; set; }
        public DuplicateData DuplicateData { get; set; }

        public GlobalDataHolder()
        {
            PartnerName = string.Empty;
            PartnerId = 0;
            UserGroup = 0;
            CasesData = null;
            ScheduleData = null;
            MailData = null;
            FileStorageData = null;
            CasesChangesData = null;
            ScheduleChangesData = null;
            MailChangesData = null;
            NotMatchingRecordsData = null;
            BrokenLinksData = null;
            CheckMatchesData = null;
            LostFilesData = null;
            DuplicateData = null;
        }

        public GlobalDataHolder(string partnerName,
            int partnerId,
            int userGroup,
            NewCasesData casesData = null,
            NewScheduleData scheduleData = null,
            NewMailData mailData = null,
            FileStorageData fileStorageData = null,
            CasesChangesData casesChangesData = null,
            ScheduleChangesData scheduleChangesData = null,
            MailChangesData mailChangesData = null,
            NotMatchingRecordsData notMatchingRecordsData = null,
            BrokenLinksData brokenLinksData = null,
            CheckMatchesData checkMatchesData = null,
            LostFilesData lostFilesData = null,
            DuplicateData duplicateData = null)
        {
            PartnerName = partnerName;
            PartnerId = partnerId;
            UserGroup = userGroup;

            if (casesData != null)
            {
                CasesData = casesData;
            }
            if (scheduleData != null)
            {
                ScheduleData = scheduleData;
            }
            if (mailData != null)
            {
                MailData = mailData;
            }
            if (fileStorageData != null)
            {
                FileStorageData = fileStorageData;
            }
            if (casesChangesData != null)
            {
                CasesChangesData = casesChangesData;
            }
            if (scheduleChangesData != null)
            {
                ScheduleChangesData = scheduleChangesData;
            }
            if (mailChangesData != null)
            {
                MailChangesData = mailChangesData;
            }
            if (notMatchingRecordsData != null)
            {
                NotMatchingRecordsData = notMatchingRecordsData;
            }
            if (brokenLinksData != null)
            {
                BrokenLinksData = brokenLinksData;
            }
            if (checkMatchesData != null)
            {
                CheckMatchesData = checkMatchesData;
            }
            if (lostFilesData != null)
            {
                LostFilesData = lostFilesData;
            }
            if (duplicateData != null)
            {
                DuplicateData = duplicateData;
            }
        }
    }
}
