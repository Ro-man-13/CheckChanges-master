//using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Pdf.IO;
using System.IO;
using System.Threading;

namespace PdfFileUpdater
{
    class Program
    {
        static void Main(string[] args)
        {
            string strDT = DateTime.Today.ToString("yyMMdd");
            string path = @"D:\Moscow\e-filing\" + strDT + "-"; // Z:
            if (Directory.Exists(path))
                UpdatePDF(path);
        }


        static bool UpdatePDF(string pdfDirect) // Spire.Pdf 5.1.0
        {
            var files = Directory.GetFiles(pdfDirect);
            foreach(var fileName in files)
            {
                bool flagNew = false;
                string oldFilePath = fileName.Replace(".pdf","_old.pdf");
                PdfDocument outputPDFDocument = new PdfDocument();
                PdfDocument PDFDocument = PdfReader.Open(fileName, PdfDocumentOpenMode.Import);
                outputPDFDocument.Version = PDFDocument.Version;
                foreach (PdfPage page in PDFDocument.Pages)
                {
                    if (HasContent(page))
                        outputPDFDocument.AddPage(page);
                    else
                        flagNew = true;
                }
                if (flagNew)
                {
                    System.IO.File.Move(fileName, oldFilePath);
                    outputPDFDocument.Save(fileName);
                    Console.WriteLine("Update - " + fileName);
                }                
            }
            Thread.Sleep(5000);
            return true;
        }

        static bool HasContent(PdfPage page)
        {
            for (var i = 0; i < page.Contents.Elements.Count; i++)
            {
                var tt = page.Contents.Elements.GetDictionary(i).Stream.Length;
                if (page.Contents.Elements.GetDictionary(i).Stream.Length > 200) // 176                
                    return true;                
            }
            return false;
        }


    }
}
