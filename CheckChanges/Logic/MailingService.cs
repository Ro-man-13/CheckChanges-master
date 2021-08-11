using CheckChanges.Models.Entities;
using CheckChanges.Models.Holders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CheckChanges.Logic
{
    class MailingService
    {
        public static void CreateMail(List<GlobalDataHolder> data, int state,List<Users> userList)
        {
            string hostName = "https://vision.mspcorporate.com/PartnerZoneNew/";
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Изменения в клиентской базе данных за ");
            stringBuilder.Append("<u>");
            stringBuilder.Append(MainService.DateString);
            stringBuilder.Append("</u><br><br>");           
            for (int i = 0; i < data.Count; i++)
            {
                if (ContainInfo(data[i]))
                {
                    stringBuilder.Append("<b><p style='font-size: 14px'>");
                    stringBuilder.Append(data[i].PartnerName);
                    stringBuilder.Append("</p></b><br>");
                    var newCasesHolders = data[i].CasesData.NewCasesList;
                    if (newCasesHolders != null && newCasesHolders.Count != 0)
                    {
                        stringBuilder.Append("<ul><u>\t");
                        stringBuilder.Append(data[i].CasesData.Data);
                        stringBuilder.Append("</u></ul>");
                        for (int j = 0; j < data[i].CasesData.NewCasesList.Count; j++)
                        {
                            stringBuilder.Append("<br><ul><ul>\t");
                            stringBuilder.Append("Было создано дело ");
                            stringBuilder.Append("<a href=");
                            stringBuilder.Append(hostName);
                            stringBuilder.Append(strEncodeURL(data[i].PartnerId.ToString()));
                            if ((data[i].UserGroup == 10 || data[i].UserGroup == 27 || data[i].UserGroup == 18) && data[i].CasesData.NewCasesList[j].Flag == 1)
                            {
                                stringBuilder.Append("/Invention/");
                            }
                            else if ((data[i].UserGroup == 10 || data[i].UserGroup == 27 || data[i].UserGroup == 18) && data[i].CasesData.NewCasesList[j].Flag == 2)
                            {
                                stringBuilder.Append("/Trademark/");
                            }
                            stringBuilder.Append(data[i].CasesData.NewCasesList[j].OurCase);
                            stringBuilder.Append(">");
                            stringBuilder.Append(data[i].CasesData.NewCasesList[j].OurCase);
                            stringBuilder.Append("</a>");
                            stringBuilder.Append(" пользователем ");
                            stringBuilder.Append(data[i].CasesData.NewCasesList[j].Creator);
                            stringBuilder.Append("</ul></ul><br>");
                        }
                        stringBuilder.Append("<br>");
                    }
                    var newScheduleHolders = data[i].ScheduleData.NewScheduleHolder;
                    if (newScheduleHolders != null && newScheduleHolders.Count != 0)
                    {
                        stringBuilder.Append("<br><ul><u>\t");
                        stringBuilder.Append(data[i].ScheduleData.Data);
                        stringBuilder.Append("</u></ul><br>");
                        for (int j = 0; j < data[i].ScheduleData.NewScheduleHolder.Count; j++)
                        {
                            stringBuilder.Append("<ul><ul>\t");
                            stringBuilder.Append("Была создана запись в таблице Schedule по делу ");
                            stringBuilder.Append("<a href=");
                            stringBuilder.Append(hostName);
                            stringBuilder.Append(strEncodeURL(data[i].PartnerId.ToString()));
                            if ((data[i].UserGroup == 10 || data[i].UserGroup == 27 || data[i].UserGroup == 18) && data[i].ScheduleData.NewScheduleHolder[j].Flag == 1)
                            {
                                stringBuilder.Append("/Invention/");
                            }
                            else if ((data[i].UserGroup == 10 || data[i].UserGroup == 27 || data[i].UserGroup == 18) && data[i].ScheduleData.NewScheduleHolder[j].Flag == 2)
                            {
                                stringBuilder.Append("/Trademark/");
                            }
                            stringBuilder.Append(data[i].ScheduleData.NewScheduleHolder[j].OurCase);
                            stringBuilder.Append(">");
                            stringBuilder.Append(data[i].ScheduleData.NewScheduleHolder[j].OurCase);
                            stringBuilder.Append("</a>");
                            stringBuilder.Append(" пользователем ");
                            stringBuilder.Append(data[i].ScheduleData.NewScheduleHolder[j].Creator);
                            stringBuilder.Append(" со следующими данными: ");
                            stringBuilder.Append("<br>\t\t");
                            stringBuilder.Append("Action: ");
                            stringBuilder.Append(data[i].ScheduleData.NewScheduleHolder[j].Action);
                            stringBuilder.Append("<br>\t\t");
                            stringBuilder.Append("Status: ");
                            stringBuilder.Append(data[i].ScheduleData.NewScheduleHolder[j].Status);
                            stringBuilder.Append("<br>\t\t");
                            stringBuilder.Append("Show_all: ");
                            stringBuilder.Append(data[i].ScheduleData.NewScheduleHolder[j].Show);
                            stringBuilder.Append("</ul></ul>");
                        }
                        stringBuilder.Append("<br>");
                    }
                    var newMailHolders = data[i].MailData.NewMailHolder;
                    if (newMailHolders != null && newMailHolders.Count != 0)
                    {
                        stringBuilder.Append("<br><ul><u>\t");
                        stringBuilder.Append(data[i].MailData.Data);
                        stringBuilder.Append("</u></ul><br>");
                        for (int j = 0; j < data[i].MailData.NewMailHolder.Count; j++)
                        {
                            stringBuilder.Append("<ul><ul>\t");
                            stringBuilder.Append("Была создана запись в таблице Mail по делу ");
                            stringBuilder.Append("<a href=");
                            stringBuilder.Append(hostName);
                            stringBuilder.Append(strEncodeURL(data[i].PartnerId.ToString()));
                            if ((data[i].UserGroup == 10 || data[i].UserGroup == 27 || data[i].UserGroup == 18) && data[i].MailData.NewMailHolder[j].Flag == 1)
                            {
                                stringBuilder.Append("/Invention/");
                            }
                            else if ((data[i].UserGroup == 10 || data[i].UserGroup == 27 || data[i].UserGroup == 18) && data[i].MailData.NewMailHolder[j].Flag == 2)
                            {
                                stringBuilder.Append("/Trademark/");
                            }
                            stringBuilder.Append(data[i].MailData.NewMailHolder[j].OurCase);
                            stringBuilder.Append(">");
                            stringBuilder.Append(data[i].MailData.NewMailHolder[j].OurCase);
                            stringBuilder.Append("</a>");
                            stringBuilder.Append(" пользователем ");
                            stringBuilder.Append(data[i].MailData.NewMailHolder[j].Creator);
                            stringBuilder.Append(" со следующими данными: ");
                            stringBuilder.Append("<br>\t\t");
                            stringBuilder.Append("Содержание: ");
                            stringBuilder.Append(data[i].MailData.NewMailHolder[j].Content);
                            stringBuilder.Append("</ul></ul><br>");
                        }
                        stringBuilder.Append("<br>");
                    }
                    var fileStorageHolders = data[i].FileStorageData.FileStorageHolder;
                    if (fileStorageHolders != null && fileStorageHolders.Count != 0)
                    {
                        stringBuilder.Append("<br><ul><u>\t");
                        stringBuilder.Append(data[i].FileStorageData.Data);
                        stringBuilder.Append("</u></ul><br>");
                        for (int j = 0; j < data[i].FileStorageData.FileStorageHolder.Count; j++)
                        {
                            stringBuilder.Append("<ul><ul>\t");
                            stringBuilder.Append("Была проведена операция с файлом (создание/изменение) в папке 'f-rep' по делу ");
                            stringBuilder.Append("<a href=");
                            stringBuilder.Append(hostName);
                            stringBuilder.Append(strEncodeURL(data[i].PartnerId.ToString()));
                            if ((data[i].UserGroup == 10 || data[i].UserGroup == 27 || data[i].UserGroup == 18) && data[i].FileStorageData.FileStorageHolder[j].Flag == 1)
                            {
                                stringBuilder.Append("/Invention/");
                            }
                            else if ((data[i].UserGroup == 10 || data[i].UserGroup == 27 || data[i].UserGroup == 18) && data[i].FileStorageData.FileStorageHolder[j].Flag == 2)
                            {
                                stringBuilder.Append("/Trademark/");
                            }
                            stringBuilder.Append(data[i].FileStorageData.FileStorageHolder[j].OurCase);
                            stringBuilder.Append(">");
                            stringBuilder.Append(data[i].FileStorageData.FileStorageHolder[j].OurCase);
                            stringBuilder.Append("</a>");
                            stringBuilder.Append("<br>\t\t");
                            stringBuilder.Append("Путь к папке: ");
                            stringBuilder.Append(data[i].FileStorageData.FileStorageHolder[j].Link);
                            stringBuilder.Append("<br>\t\t");
                            stringBuilder.Append("Имя файла: ");
                            stringBuilder.Append(data[i].FileStorageData.FileStorageHolder[j].Content);
                            stringBuilder.Append("</ul></ul><br>");
                        }
                        stringBuilder.Append("<br>");
                    }
                    var casesChangesHolder = data[i].CasesChangesData.CasesChangesHolder;
                    if (casesChangesHolder != null && casesChangesHolder.Count != 0)
                    {
                        stringBuilder.Append("<br><ul><u>\t");
                        stringBuilder.Append(data[i].CasesChangesData.Data);
                        stringBuilder.Append("</u></ul><br>");
                        for (int j = 0; j < data[i].CasesChangesData.CasesChangesHolder.Count; j++)
                        {
                            stringBuilder.Append("<ul><ul>\t");
                            stringBuilder.Append("Было изменено дело ");
                            stringBuilder.Append("<a href=");
                            stringBuilder.Append(hostName);
                            stringBuilder.Append(strEncodeURL(data[i].PartnerId.ToString()));
                            if ((data[i].UserGroup == 10 || data[i].UserGroup == 27 || data[i].UserGroup == 18) && data[i].CasesChangesData.CasesChangesHolder[j].Flag == 1)
                            {
                                stringBuilder.Append("/Invention/");
                            }
                            else if ((data[i].UserGroup == 10 || data[i].UserGroup == 27 || data[i].UserGroup == 18) && data[i].CasesChangesData.CasesChangesHolder[j].Flag == 2)
                            {
                                stringBuilder.Append("/Trademark/");
                            }
                            stringBuilder.Append(data[i].CasesChangesData.CasesChangesHolder[j].OurCase);
                            stringBuilder.Append(">");
                            stringBuilder.Append(data[i].CasesChangesData.CasesChangesHolder[j].OurCase);
                            stringBuilder.Append("</a>");
                            stringBuilder.Append(" пользователем ");
                            stringBuilder.Append(data[i].CasesChangesData.CasesChangesHolder[j].Last_User);
                            stringBuilder.Append("</ul></ul><br>");
                        }
                        stringBuilder.Append("<br>");
                    }
                    var scheduleChangesHolder = data[i].ScheduleChangesData.ScheduleChangesHolder;
                    if (scheduleChangesHolder != null && scheduleChangesHolder.Count != 0)
                    {
                        stringBuilder.Append("<br><ul><u>\t");
                        stringBuilder.Append(data[i].ScheduleChangesData.Data);
                        stringBuilder.Append("</u></ul><br>");
                        for (int j = 0; j < data[i].ScheduleChangesData.ScheduleChangesHolder.Count; j++)
                        {
                            stringBuilder.Append("<ul><ul>\t");
                            stringBuilder.Append("Была изменена запись в таблице Schedule по делу ");
                            stringBuilder.Append("<a href=");
                            stringBuilder.Append(hostName);
                            stringBuilder.Append(strEncodeURL(data[i].PartnerId.ToString()));
                            if ((data[i].UserGroup == 10 || data[i].UserGroup == 27 || data[i].UserGroup == 18) && data[i].ScheduleChangesData.ScheduleChangesHolder[j].Flag == 1)
                            {
                                stringBuilder.Append("/Invention/");
                            }
                            else if ((data[i].UserGroup == 10 || data[i].UserGroup == 27 || data[i].UserGroup == 18) && data[i].ScheduleChangesData.ScheduleChangesHolder[j].Flag == 2)
                            {
                                stringBuilder.Append("/Trademark/");
                            }
                            stringBuilder.Append(data[i].ScheduleChangesData.ScheduleChangesHolder[j].OurCase);
                            stringBuilder.Append(">");
                            stringBuilder.Append(data[i].ScheduleChangesData.ScheduleChangesHolder[j].OurCase);
                            stringBuilder.Append("</a>");
                            stringBuilder.Append(" пользователем ");
                            stringBuilder.Append(data[i].ScheduleChangesData.ScheduleChangesHolder[j].ChangedBy);
                            stringBuilder.Append("</ul></ul><br>");
                        }
                        stringBuilder.Append("<br>");
                    }
                    var mailChangesHolder = data[i].MailChangesData.MailChangesHolder;
                    if (mailChangesHolder != null && mailChangesHolder.Count != 0)
                    {
                        stringBuilder.Append("<br><ul><u>\t");
                        stringBuilder.Append(data[i].MailChangesData.Data);
                        stringBuilder.Append("</u></ul><br>");
                        for (int j = 0; j < data[i].MailChangesData.MailChangesHolder.Count; j++)
                        {
                            stringBuilder.Append("<ul><ul>\t");
                            stringBuilder.Append("Была изменена запись в таблице Mail по делу ");
                            stringBuilder.Append("<a href=");
                            stringBuilder.Append(hostName);
                            stringBuilder.Append(strEncodeURL(data[i].PartnerId.ToString()));
                            if ((data[i].UserGroup == 10 || data[i].UserGroup == 27 || data[i].UserGroup == 18) && data[i].MailChangesData.MailChangesHolder[j].Flag == 1)
                            {
                                stringBuilder.Append("/Invention/");
                            }
                            else if ((data[i].UserGroup == 10 || data[i].UserGroup == 27 || data[i].UserGroup == 18) && data[i].MailChangesData.MailChangesHolder[j].Flag == 2)
                            {
                                stringBuilder.Append("/Trademark/");
                            }
                            stringBuilder.Append(data[i].MailChangesData.MailChangesHolder[j].OurCase);
                            stringBuilder.Append(">");
                            stringBuilder.Append(data[i].MailChangesData.MailChangesHolder[j].OurCase);
                            stringBuilder.Append("</a>");
                            stringBuilder.Append(" пользователем ");
                            stringBuilder.Append(data[i].MailChangesData.MailChangesHolder[j].ChangedBy);
                            stringBuilder.Append("</ul></ul><br>");
                        }
                        stringBuilder.Append("<br>");
                    }
                    var notMatchingRecordsHolder = data[i].NotMatchingRecordsData.NotMatchingRecordsHolder;
                    if (notMatchingRecordsHolder != null && notMatchingRecordsHolder.Count != 0)
                    {
                        stringBuilder.Append("<br><ul><u style='background-color:#000; color:#fff'>\t");
                        stringBuilder.Append(data[i].NotMatchingRecordsData.Data);
                        stringBuilder.Append("</u></ul><br>");
                        for (int j = 0; j < data[i].NotMatchingRecordsData.NotMatchingRecordsHolder.Count; j++)
                        {
                            stringBuilder.Append("<ul><ul>\t");
                            stringBuilder.Append("К делу ");
                            stringBuilder.Append("<a href=");
                            stringBuilder.Append(hostName);
                            stringBuilder.Append(strEncodeURL(data[i].PartnerId.ToString()));
                            if ((data[i].UserGroup == 10 || data[i].UserGroup == 27 || data[i].UserGroup == 18) && data[i].NotMatchingRecordsData.NotMatchingRecordsHolder[j].Flag == 1)
                            {
                                stringBuilder.Append("/Invention/");
                            }
                            else if ((data[i].UserGroup == 10 || data[i].UserGroup == 27 || data[i].UserGroup == 18) && data[i].NotMatchingRecordsData.NotMatchingRecordsHolder[j].Flag == 2)
                            {
                                stringBuilder.Append("/Trademark/");
                            }
                            stringBuilder.Append(data[i].NotMatchingRecordsData.NotMatchingRecordsHolder[j].OurSheduleCase);
                            stringBuilder.Append(">");
                            stringBuilder.Append(data[i].NotMatchingRecordsData.NotMatchingRecordsHolder[j].OurSheduleCase);
                            stringBuilder.Append("</a>");
                            stringBuilder.Append(" из таблицы Schedule привязана неправильная входящая корреспонденция из дела ");
                            stringBuilder.Append(data[i].NotMatchingRecordsData.NotMatchingRecordsHolder[j].OurMailCase);
                            stringBuilder.Append(" таблицы Mail по номеру: ");
                            stringBuilder.Append(data[i].NotMatchingRecordsData.NotMatchingRecordsHolder[j].RegNo);
                            stringBuilder.Append("</ul></ul><br>");
                        }
                        stringBuilder.Append("<br>");
                    }
                    var brokenLinksHolder = data[i].BrokenLinksData.BrokenLinksHolder;
                    if (brokenLinksHolder != null && brokenLinksHolder.Count != 0)
                    {
                        stringBuilder.Append("<br><ul><u style='background-color:#f00; color:#fff'>\t");
                        stringBuilder.Append(data[i].BrokenLinksData.Data);
                        stringBuilder.Append("</u></ul><br>");
                        for (int j = 0; j < data[i].BrokenLinksData.BrokenLinksHolder.Count; j++)
                        {
                            stringBuilder.Append("<ul><ul>\t");
                            stringBuilder.Append("Была обнаружена неправильная ссылка по делу ");
                            stringBuilder.Append("<a href=");
                            stringBuilder.Append(hostName);
                            stringBuilder.Append(strEncodeURL(data[i].PartnerId.ToString()));
                            if ((data[i].UserGroup == 10 || data[i].UserGroup == 27 || data[i].UserGroup == 18) && data[i].BrokenLinksData.BrokenLinksHolder[j].Flag == 1)
                            {
                                stringBuilder.Append("/Invention/");
                            }
                            else if ((data[i].UserGroup == 10 || data[i].UserGroup == 27 || data[i].UserGroup == 18) && data[i].BrokenLinksData.BrokenLinksHolder[j].Flag == 2)
                            {
                                stringBuilder.Append("/Trademark/");
                            }
                            stringBuilder.Append(data[i].BrokenLinksData.BrokenLinksHolder[j].OurCase);
                            stringBuilder.Append(">");
                            stringBuilder.Append(data[i].BrokenLinksData.BrokenLinksHolder[j].OurCase);
                            stringBuilder.Append("</a>");
                            stringBuilder.Append("<br>\t\t");
                            stringBuilder.Append("Action: ");
                            stringBuilder.Append(data[i].BrokenLinksData.BrokenLinksHolder[j].Action);
                            stringBuilder.Append("<br>\t\t");
                            stringBuilder.Append("Link: ");
                            stringBuilder.Append(data[i].BrokenLinksData.BrokenLinksHolder[j].Link);
                            stringBuilder.Append("</ul></ul><br>");
                        }
                        stringBuilder.Append("<br>");
                    }
                    //var duplicateHolder = data[i].DuplicateData.DuplicateHolder;
                    //if (duplicateHolder != null && duplicateHolder.Count != 0)
                    //{
                    //    stringBuilder.Append("<br><ul><u style='background-color:#f00; color:#fff'>\t");
                    //    stringBuilder.Append(data[i].DuplicateData.Data);
                    //    stringBuilder.Append("</u></ul><br>");
                    //    for (int j = 0; j < data[i].DuplicateData.DuplicateHolder.Count; j++)
                    //    {
                    //        stringBuilder.Append("<ul><ul>\t");
                    //        stringBuilder.Append("Было обнаружено повторение описания Filing Request по делу ");
                    //        stringBuilder.Append("<a href=");
                    //        stringBuilder.Append(hostName);
                    //        stringBuilder.Append(strEncodeURL(data[i].PartnerId.ToString()));
                    //        if ((data[i].UserGroup == 10 || data[i].UserGroup == 27 || data[i].UserGroup == 18) && data[i].DuplicateData.DuplicateHolder[j].Flag == 1)
                    //        {
                    //            stringBuilder.Append("/Invention/");
                    //        }
                    //        else if ((data[i].UserGroup == 10 || data[i].UserGroup == 27 || data[i].UserGroup == 18) && data[i].DuplicateData.DuplicateHolder[j].Flag == 2)
                    //        {
                    //            stringBuilder.Append("/Trademark/");
                    //        }
                    //        stringBuilder.Append(data[i].DuplicateData.DuplicateHolder[j].OurCase);
                    //        stringBuilder.Append(">");
                    //        stringBuilder.Append(data[i].DuplicateData.DuplicateHolder[j].OurCase);
                    //        stringBuilder.Append("</a>");
                    //        stringBuilder.Append("</ul></ul><br>");
                    //    }
                    //    stringBuilder.Append("<br>");
                    //}
                    var lostFilesHolder = data[i].LostFilesData.LostFilesHolder;
                    if (lostFilesHolder != null && lostFilesHolder.Count != 0)
                    {
                        stringBuilder.Append("<br><ul><u>\t");
                        stringBuilder.Append(data[i].LostFilesData.Data);
                        stringBuilder.Append("</u></ul><br>");
                        for (int j = 0; j < data[i].LostFilesData.LostFilesHolder.Count; j++)
                        {
                            stringBuilder.Append("<ul><ul>\t");
                            stringBuilder.Append("Отстуствует документ по делу ");
                            stringBuilder.Append("<a href=");
                            stringBuilder.Append(hostName);
                            stringBuilder.Append(strEncodeURL(data[i].PartnerId.ToString()));
                            if ((data[i].UserGroup == 10 || data[i].UserGroup == 27 || data[i].UserGroup == 18) && data[i].LostFilesData.LostFilesHolder[j].Flag == 1)
                            {
                                stringBuilder.Append("/Invention/");
                            }
                            else if ((data[i].UserGroup == 10 || data[i].UserGroup == 27 || data[i].UserGroup == 18) && data[i].LostFilesData.LostFilesHolder[j].Flag == 2)
                            {
                                stringBuilder.Append("/Trademark/");
                            }
                            stringBuilder.Append(data[i].LostFilesData.LostFilesHolder[j].OurCase);
                            stringBuilder.Append(">");
                            stringBuilder.Append(data[i].LostFilesData.LostFilesHolder[j].OurCase);
                            stringBuilder.Append("</a>");
                            stringBuilder.Append("<br>\t\t");
                            stringBuilder.Append("Путь к папке: ");
                            stringBuilder.Append(data[i].LostFilesData.LostFilesHolder[j].Link);
                            stringBuilder.Append("<br>\t\t");
                            stringBuilder.Append("Имя файла: ");
                            stringBuilder.Append(data[i].LostFilesData.LostFilesHolder[j].File);
                            stringBuilder.Append("</ul></ul><br>");
                        }
                        stringBuilder.Append("<br>");
                    }
                    var checkMatchesHolder = data[i].CheckMatchesData.CheckMatchesHolder;
                    if (checkMatchesHolder != null && checkMatchesHolder.Count != 0)
                    {
                        stringBuilder.Append("<br><ul><u style='background-color:#f00; color:#fff'>\t");
                        stringBuilder.Append(data[i].CheckMatchesData.Data);
                        stringBuilder.Append("</u></ul><br>");
                        for (int j = 0; j < data[i].CheckMatchesData.CheckMatchesHolder.Count; j++)
                        {
                            stringBuilder.Append("<ul><ul>\t");
                            stringBuilder.Append("Было обнаружено отсутствие содержания по делу ");
                            stringBuilder.Append("<a href=");
                            stringBuilder.Append(hostName);
                            stringBuilder.Append(strEncodeURL(data[i].PartnerId.ToString()));
                            if ((data[i].UserGroup == 10 || data[i].UserGroup == 27 || data[i].UserGroup == 18) && data[i].CheckMatchesData.CheckMatchesHolder[j].Flag == 1)
                            {
                                stringBuilder.Append("/Invention/");
                            }
                            else if ((data[i].UserGroup == 10 || data[i].UserGroup == 27 || data[i].UserGroup == 18) && data[i].CheckMatchesData.CheckMatchesHolder[j].Flag == 2)
                            {
                                stringBuilder.Append("/Trademark/");
                            }
                            stringBuilder.Append(data[i].CheckMatchesData.CheckMatchesHolder[j].OurCase);
                            stringBuilder.Append(">");
                            stringBuilder.Append(data[i].CheckMatchesData.CheckMatchesHolder[j].OurCase);
                            stringBuilder.Append("</a>");
                            stringBuilder.Append("<br>\t\t");
                            stringBuilder.Append("Source: ");
                            stringBuilder.Append(data[i].CheckMatchesData.CheckMatchesHolder[j].Source);
                            stringBuilder.Append("<br>\t\t");
                            stringBuilder.Append("Action: ");
                            stringBuilder.Append(data[i].CheckMatchesData.CheckMatchesHolder[j].Action);
                            stringBuilder.Append("</ul></ul><br>");
                        }
                        stringBuilder.Append("<br>");
                    }
                }
            }

            Sendmail(stringBuilder.ToString(), state, userList);
        }

        static void Sendmail(string body, int state, List<Users> userList)
        {
            
            var mail = new MailMessage { From = new MailAddress("noreply@msp.ua") };

            var mailList = userList.Select(x =>new MailAddress(x.email)).ToList();

            if (state == 1) // Inventions
            {

                foreach (var item in mailList) {
                    mail.To.Add(item);
                }
                //mail.To.Add(new MailAddress("n.sikorinskaya@mspcorporate.com"));
                // mail.To.Add(new MailAddress("webmaster@vision.msp.ua"));

            }
            else if (state == 2) // TM
            {
                foreach (var item in mailList)
                {
                    mail.To.Add(item);
                }
               // mail.To.Add(new MailAddress("e.guryanova@mspcorporate.com"));
                //mail.To.Add(new MailAddress("webmaster@vision.msp.ua"));
            } 
            ////mail.To.Add(new MailAddress("d.sereda@msp.ua"));
            ////mail.To.Add(new MailAddress("yulia.klimova@msp.ua"));
            //mail.To.Add(new MailAddress("webmaster@vision.msp.ua"));


            //mail.To.Add(new MailAddress("guryanova@msp.ua"));
            //mail.To.Add(new MailAddress("n.sikorinskaya@msp.ua"));
            //mail.To.Add(new MailAddress("miroslav@msp.ua"));
            
            mail.Subject = "Изменения в открытой базе данных";
            mail.Body = body;
            mail.IsBodyHtml = true;
            mail.BodyEncoding = Encoding.UTF8;
            var smtp = new SmtpClient
            {
                Host = "mx1.mirohost.net",
                Port = 25,
                //EnableSsl = true,
                Credentials = new NetworkCredential("noreply@msp.ua", "qw341234"),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
            try
            {
                //throw new SmtpException();
                smtp.Send(mail);
            }
            catch (Exception exp)
            {
                if (File.Exists(DateTime.Now.ToString("yyyy-MM-dd") + "_CheckChanges_err.log"))
                {
                    using (
                        FileStream fs = new FileStream(DateTime.Now.ToString("yyyy-MM-dd") + "_CheckChanges_err.log",
                            FileMode.Append))
                    {
                        StreamWriter sw = new StreamWriter(fs);
                        sw.Write(exp.Message);
                        sw.Close();
                    }
                }
                else
                {
                    using (
                        FileStream fs = new FileStream(DateTime.Now.ToString("yyyy-MM-dd") + "_CheckChanges_err.log",
                            FileMode.OpenOrCreate))
                    {
                        StreamWriter sw = new StreamWriter(fs);
                        sw.Write(exp.Message);
                        sw.Close();
                    }
                }
            }
        }

        static string strEncodeURL(string str) {
            if (str != null && str != "") {
                str = str.Replace(" ", "%20");
                str = str.Replace("&", "%26");
            }            
            return str;
        }

        static bool ContainInfo(GlobalDataHolder holder)
        {
            return (holder.CasesData.NewCasesList != null && holder.CasesData.NewCasesList.Count != 0) ||
                   (holder.ScheduleData.NewScheduleHolder != null && holder.ScheduleData.NewScheduleHolder.Count != 0) ||
                   (holder.MailData.NewMailHolder != null && holder.MailData.NewMailHolder.Count != 0) ||
                   (holder.FileStorageData.FileStorageHolder != null && holder.FileStorageData.FileStorageHolder.Count != 0) ||
                   (holder.CasesChangesData.CasesChangesHolder != null && holder.CasesChangesData.CasesChangesHolder.Count != 0) ||
                   (holder.ScheduleChangesData.ScheduleChangesHolder != null && holder.ScheduleChangesData.ScheduleChangesHolder.Count != 0) ||
                   (holder.MailChangesData.MailChangesHolder != null && holder.MailChangesData.MailChangesHolder.Count != 0) ||
                   (holder.NotMatchingRecordsData.NotMatchingRecordsHolder != null && holder.NotMatchingRecordsData.NotMatchingRecordsHolder.Count != 0) ||
                   (holder.BrokenLinksData.BrokenLinksHolder != null && holder.BrokenLinksData.BrokenLinksHolder.Count != 0) ||
                   (holder.DuplicateData.DuplicateHolder != null && holder.DuplicateData.DuplicateHolder.Count != 0) ||
                   (holder.LostFilesData.LostFilesHolder != null && holder.LostFilesData.LostFilesHolder.Count != 0) ||
                   (holder.CheckMatchesData.CheckMatchesHolder != null && holder.CheckMatchesData.CheckMatchesHolder.Count != 0);

        }
    }
}
