using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace WCF.CM
{
    public class CM_Utilisateur
    {
        private WCF.CAD.CAD oCAD;
        private WCF.EM.EM_Utilisateur oUtilisateur;
        private System.Data.DataSet oDS;
        private bool result;

        public CM_Utilisateur()
        {
            this.oCAD = new WCF.CAD.CAD();
            this.oUtilisateur = new WCF.EM.EM_Utilisateur();

        }

        public System.Data.DataSet CM_selectUser(string rows, string login, string mdp)
        {
            this.oDS = this.oCAD.getRows(this.oUtilisateur.SelectUser(login, mdp), rows);
            return this.oDS;
        }

        public bool CM_sendMail(String adressMailReceiver, string subject, string body)
        {
            this.result = false;

            //Création du message
            string to = adressMailReceiver;
            string from = "valentin.carle@gmail.com";
            
            MailMessage message = new MailMessage(from, to, subject, body);


            //client smtp

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("valentin.carle@gmail.com", "mot_de_passe");

            //gestion de la pièce jointe
            Attachment maPieceJointe = new Attachment(@"C:\Pdftest.pdf");
            message.Attachments.Add(maPieceJointe);

            try
            {
                client.Send(message);
                this.result = true;
            }

            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in CreateTestMessage(): {0}", ex.ToString());
            }

            return this.result;

            
        }




    }
}