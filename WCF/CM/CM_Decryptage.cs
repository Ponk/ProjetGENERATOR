using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WCF.CM
{
    public class CM_Decryptage
    {
        private bool result;


        public bool CM_generatePdf(string titre)
        {



            string cheminFichier = "C:\\Pdftest.pdf";
            int i = 0;

            while (File.Exists(cheminFichier))
            {
                i++;

                cheminFichier = "C:\\Pdftest" + i + ".pdf";



            }

            System.IO.FileStream fs = new FileStream(cheminFichier, FileMode.Create);

            Document document = new Document(PageSize.A4, 25, 25, 30, 30);

            PdfWriter writer = PdfWriter.GetInstance(document, fs);

            document.AddAuthor("Groupe 1 eXia");
            document.AddTitle(titre);

            document.Open();
            document.Add(new Paragraph("Hello fdfs!"));
            document.Close();
            writer.Close();
            fs.Close();

            this.result = true;

            return this.result;

            
            /*System.IO.FileStream fs = new FileStream("C:\\Pdftest.pdf", FileMode.Create);

            Document document = new Document(PageSize.A4, 25, 25, 30, 30);

            PdfWriter writer = PdfWriter.GetInstance(document, fs);

            document.AddAuthor("Groupe 1 eXia");
            document.AddTitle("Titre");

            document.Open();
            document.Add(new Paragraph("Hello World!"));
            document.Close();
            writer.Close();
            fs.Close();*/

        }


    }
}