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
    class ClientFolderService
    {
        nemo_freshEntities db;

        public ClientFolderService(nemo_freshEntities db)
        {
            this.db = db;
        }

        public void GetClientFolder(int id)
        {
            Console.WriteLine("---New Files in File Storage---");
            List<string> docFoldersList = new List<string>();

            string sql = "SELECT DocumentFolders.DocumentFolder FROM DocumentFolders WHERE (((DocumentFolders.client_id)=" + id + "))";

            var docFolder = db.Database.SqlQuery<DocumentFold>(sql);



            foreach (var doc in docFolder)
            {

                string path = @"\\samson\ip-all\--MVI\" + doc.DocumentFolder;
                if (!Directory.Exists(path))
                {
                    path = @"\\samson\ip-all\--MVI\UKRAINE\" + doc.DocumentFolder;
                }
                docFoldersList.Add(!Directory.Exists(path) ? null : path);
            }

         

            foreach (string s in docFoldersList)
            {
                DirSearch(s);
            }
        }

        private void DirSearch(string sDir) 
        { 
            int j = 0; //Это счетчик измененных файлов
            if (sDir != null && sDir.Length >= 256) {
                string bigpath = sDir;
            }

            if (sDir != null&&sDir.Length<256)
            {
                foreach (string d in Directory.GetDirectories(sDir)) //Для каждой вложенной папки
                {
                    string tmp = d.ToLower().Substring(d.LastIndexOf('\\')); //Получаем название вложенной папки
                    if (tmp == "\\f-rep")
                    {
                        if (File.GetLastWriteTime(d).Date == MainService.Day) //Если папка изменялась в указанный день
                        {
                            var di = new DirectoryInfo(d);
                            bool success = int.TryParse(Regex.Replace(di.Parent.ToString(), @"[^\d]+", ""), out int r);
                            // int r = Convert.ToInt32(Regex.Replace(di.Parent.ToString(), @"[^\d]+", "")); //Из названия родительской папки получаем числа 
                            if (success)
                            {
                                string rPre = di.Parent.Name.ToString().Substring(0, 1).ToUpper(); //Из названия родительской папки получаем первую букву 

                                for (int k = 0; k < MainService.CasesList.Count; k = k + 2) //Из выбранных в начале кейсов для партнера сверяем, совпадают ли числа из названия с числами в кейсах
                                {
                                    if (Convert.ToInt32(Regex.Replace(MainService.CasesList[k], @"[^\d]+", "")) == r && MainService.CasesList[k].Contains(rPre)) //Если да, то перебираем все пдф файлы и записываем их
                                    {
                                        foreach (var file in Directory.GetFiles(d, "*.pdf"))
                                        {
                                            string action = Path.GetFileNameWithoutExtension(file);
                                            //Если экшен равен какому-то из списка, то необходимо получить шедул по экшену и кейсу. Проверяем статус на соответствие одному из списка
                                            //Если статус совпадает то ищем файл с указанным названием
                                            if (File.GetCreationTime(file) == MainService.Day)
                                            {
                                                j++;
                                                Console.WriteLine(MainService.CasesList[k]);
                                                Console.WriteLine(d);
                                                Console.WriteLine(action);
                                                MainService.fileStorage.Add(new FileStorageHolder(MainService.CasesList[k], Convert.ToInt32(MainService.CasesList[k + 1]), d, action));
                                            }
                                        }
                                    }
                                }
                                if (j == 0) //Папка изменялась, но файлы не найдены
                                {
                                    for (int k = 0; k < MainService.CasesList.Count; k = k + 2)
                                    {
                                        if (Convert.ToInt32(Regex.Replace(MainService.CasesList[k], @"[^\d]+", "")) == r)
                                        {
                                            Console.WriteLine(MainService.CasesList[k]);
                                            Console.WriteLine(d);
                                            MainService.fileStorage.Add(new FileStorageHolder(MainService.CasesList[k], Convert.ToInt32(MainService.CasesList[k + 1]), d, "Папка 'f-rep' была изменена, проверьте ее вручную"));
                                        }
                                    }
                                }
                            }
                        }
                        var dir = new DirectoryInfo(d);
                        int r1;
                        string r1Pre="";
                        try
                        {
                            r1 = Convert.ToInt32(Regex.Replace(dir.Parent.ToString(), @"[^\d]+", ""));
                            r1Pre = dir.Parent.Name.ToString().Substring(0, 1).ToUpper();
                        }
                        catch (Exception)
                        {
                            try
                            {
                                r1 = Convert.ToInt32(Regex.Replace(dir.Parent.Parent.ToString(), @"[^\d]+", ""));
                                r1Pre = dir.Parent.Parent.Name.ToString().ToUpper().Substring(0, 1);
                            }
                            catch (Exception)
                            {
                                r1 = Convert.ToInt32(Regex.Replace(dir.Parent.Parent.Parent.ToString(), @"[^\d]+", ""));
                                r1Pre = dir.Parent.Parent.Parent.Name.ToString().ToUpper().Substring(0, 1);
                            }
                        }

                        for (int l = 0; l < MainService.CasesList.Count; l = l + 2) // А вот это уже для пдф в корне папки
                        {
                            if (Convert.ToInt32(Regex.Replace(MainService.CasesList[l], @"[^\d]+", "")) == r1 && MainService.CasesList[l].Contains(r1Pre))
                            {
                                foreach (var file in Directory.GetFiles(d, "*.pdf"))
                                {
                                    MainService.SignsList.Add(MainService.CasesList[l]);
                                    MainService.SignsList.Add(Path.GetFileNameWithoutExtension(file));
                                    MainService.SignsList.Add("File Storage");
                                    MainService.SignsList.Add(MainService.CasesList[l + 1]);
                                    MainService.SignsList.Add(false.ToString());
                                }
                            }
                        }
                    }
                    DirSearch(d);
                }
            }
        }
    }
}
