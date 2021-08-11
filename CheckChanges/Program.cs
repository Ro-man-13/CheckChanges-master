using CheckChanges.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckChanges
{
    class Program
    {
        static void Main(string[] args)
        {
            //Для получения информации по конкретной дате - раскомментировать следующие 2 строки  и закомментировать присовение DateString текущей даты
            Console.Write("Введите дату (dd.mm.yyyy) и нажмите Enter: ");
            string dateString = Console.ReadLine();

            //string dateString = DateTime.Now.ToShortDateString();
            Console.WriteLine(dateString);
            string enteredDate = "";
            if (dateString != string.Empty)
            {
                //EnteredDate = Convert.ToDateTime(DateString).ToString("yyyy-MM-dd");
                enteredDate = Convert.ToDateTime(dateString).ToString("dd.MM.yyyy");
            }
            else
            {
                dateString = DateTime.Today.ToShortDateString();
                //EnteredDate = DateTime.Today.ToString("yyyy-MM-dd");
                enteredDate = Convert.ToDateTime(dateString).ToString("dd.MM.yyyy");
            }
            MainService.StartProgram(dateString, enteredDate);
        }
    }
}
