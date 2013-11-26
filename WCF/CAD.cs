using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace WCF
{
    public class CAD
    {

        
        private string serveur ;
        private string bdd;
        private string login;
        private string mdp;

        private string rq_sql;
        private SqlConnection maConnexion;
        private SqlCommand maCommande;
        private System.Data.DataSet oDS;
        private SqlDataAdapter oDA;
        
        public CAD()
        {
            this.rq_sql = null;
            this.maCommande = null;
            this.serveur = "TRISTANJOLY2C5F";
            this.bdd = "bdd_generator";
            this.login = "valhunter";
            this.mdp = "valhunter";
            

            maConnexion = new SqlConnection("Data Source= '" + serveur + "';Initial Catalog= '" + bdd + "';User Id= '" + login + "';Password= '" + mdp + "'");

        }

        public System.Data.DataSet getRows(string rq_sql, string rows)
        {
            this.oDS = new System.Data.DataSet();
            this.rq_sql = rq_sql;
            this.maCommande = new SqlCommand(this.rq_sql, this.maConnexion);
            this.oDA = new SqlDataAdapter(this.maCommande);
            this.oDA.Fill(this.oDS, "rows");
            return this.oDS;

        }



            public void generatePdf()
            {
                System.IO.FileStream fs = new FileStream("C:\\Pdftest.pdf", FileMode.Create);

                Document document = new Document(PageSize.A4, 25, 25, 30, 30);

                PdfWriter writer = PdfWriter.GetInstance(document, fs);

                document.AddAuthor("Groupe 1 eXia");
                document.AddTitle("Titre");

                document.Open();
                document.Add(new Paragraph("Hello World!"));
                document.Close();
                writer.Close();
                fs.Close();

            }

        /// <summary>
        /// Send Mail for User by SMTP
        /// </summary>
        /// <param name="Contenu">Mail Boby</param>
        /// <param name="Subject">Mail subject</param>
        /// <param name="MailTo">Mail to adress</param>
        /// <param name="MailFrom">Mail from adress</param>
        /// <param name="SMTPServer">SMTP Server to use</param>
        /// <param name="InHTML">Body Mail in HTML or not</param>
        /// <returns>Send Mail sent OK ou Error</returns>

            /*public string sendMail()
            {
                
                MailSettings.SMTPServer = Convert.ToString(ConfigurationManager.AppSettings["HostName"]);
                MailMessage Msg = new MailMessage();
                // Sender e-mail address.
                Msg.From = new MailAddress("pqr@gmail.com");
                // Recipient e-mail address.
                Msg.To.Add("abc@gmail.com");
                Msg.CC.Add("zcd@gmail.com");
                Msg.Subject = "Timesheet Payment Instruction updated";
                Msg.IsBodyHtml = true;
                Msg.Body = emailMessage.ToString();
                NetworkCredential loginInfo = new NetworkCredential(Convert.ToString(ConfigurationManager.AppSettings["UserName"]), Convert.ToString(ConfigurationManager.AppSettings["Password"])); // password for connection smtp if u dont have have then pass blank

                SmtpClient smtp = new SmtpClient();
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = loginInfo;
                //smtp.EnableSsl = true;
                //No need for port
                //smtp.Host = ConfigurationManager.AppSettings["HostName"];
                //smtp.Port = int.Parse(ConfigurationManager.AppSettings["PortNumber"]);
                smtp.Send(Msg);

                

                return email.ToString();

            }*/


               
            

            
            
        


    }
}